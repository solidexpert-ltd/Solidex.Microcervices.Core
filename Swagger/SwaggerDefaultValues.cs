using System;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Solidex.Microservices.Core.Swagger
{
    /// <summary>
    /// Represents the OpenAPI/Swashbuckle operation filter used to document information provided, but not used.
    /// </summary>
    /// <remarks>This <see cref="IOperationFilter"/> is only required due to bugs in the <see cref="SwaggerGenerator"/>.
    /// Once they are fixed and published, this class can be removed.</remarks>
    public class SwaggerDefaultValues : IOperationFilter
    {
        /// <inheritdoc />
        public void Apply( OpenApiOperation operation, OperationFilterContext context )
        {
            var apiDescription = context.ApiDescription;

        //    operation.Deprecated |= apiDescription.IsDeprecated();

            // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1752#issue-663991077
            foreach ( var responseType in context.ApiDescription.SupportedResponseTypes )
            {
                // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/b7cf75e7905050305b115dd96640ddd6e74c7ac9/src/Swashbuckle.AspNetCore.SwaggerGen/SwaggerGenerator/SwaggerGenerator.cs#L383-L387
                var responseKey = responseType.IsDefaultResponse ? "default" : responseType.StatusCode.ToString();
                var response = operation.Responses[responseKey];

                foreach ( var contentType in response.Content.Keys )
                {
                    if ( !responseType.ApiResponseFormats.Any( x => x.MediaType == contentType ) )
                    {
                        response.Content.Remove( contentType );
                    }
                }
            }

            if ( operation.Parameters == null )
            {
                return;
            }

            // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/412
            // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/pull/413
            foreach ( var parameter in operation.Parameters )
            {
                var description = apiDescription.ParameterDescriptions.First( p => p.Name == parameter.Name );

                parameter.Description ??= description.ModelMetadata?.Description;

                if ( parameter.Schema.Default == null &&
                     description.DefaultValue != null &&
                     description.DefaultValue is not DBNull &&
                     description.ModelMetadata is ModelMetadata modelMetadata )
                {
                    // REF: https://github.com/Microsoft/aspnet-api-versioning/issues/429#issuecomment-605402330
                    var json = JsonSerializer.Serialize( description.DefaultValue, modelMetadata.ModelType );
                    parameter.Schema.Default = CreateOpenApiAnyFromJson( json );
                }

                parameter.Required |= description.IsRequired;
            }
        }

        /// <summary>
        /// Creates an IOpenApiAny instance from a JSON string.
        /// This replaces the missing OpenApiAnyFactory.CreateFromJson method.
        /// </summary>
        private static IOpenApiAny CreateOpenApiAnyFromJson( string json )
        {
            var element = JsonSerializer.Deserialize<JsonElement>( json );
            return CreateFromJsonElement( element );
        }

        /// <summary>
        /// Converts a JsonElement to the appropriate IOpenApiAny type.
        /// </summary>
        private static IOpenApiAny CreateFromJsonElement( JsonElement element )
        {
            return element.ValueKind switch
            {
                JsonValueKind.String => new OpenApiString( element.GetString() ?? string.Empty ),
                JsonValueKind.Number => element.TryGetInt32( out var intValue )
                    ? new OpenApiInteger( intValue )
                    : element.TryGetInt64( out var longValue )
                        ? new OpenApiLong( longValue )
                        : new OpenApiDouble( element.GetDouble() ),
                JsonValueKind.True => new OpenApiBoolean( true ),
                JsonValueKind.False => new OpenApiBoolean( false ),
                JsonValueKind.Null => new OpenApiNull(),
                JsonValueKind.Array => CreateOpenApiArray( element ),
                JsonValueKind.Object => CreateOpenApiObject( element ),
                _ => throw new ArgumentException( $"Unsupported JSON value kind: {element.ValueKind}" )
            };
        }

        /// <summary>
        /// Creates an OpenApiArray from a JsonElement array.
        /// </summary>
        private static OpenApiArray CreateOpenApiArray( JsonElement element )
        {
            var array = new OpenApiArray();
            foreach ( var item in element.EnumerateArray() )
            {
                array.Add( CreateFromJsonElement( item ) );
            }
            return array;
        }

        /// <summary>
        /// Creates an OpenApiObject from a JsonElement object.
        /// </summary>
        private static OpenApiObject CreateOpenApiObject( JsonElement element )
        {
            var obj = new OpenApiObject();
            foreach ( var property in element.EnumerateObject() )
            {
                obj[property.Name] = CreateFromJsonElement( property.Value );
            }
            return obj;
        }
    }
}