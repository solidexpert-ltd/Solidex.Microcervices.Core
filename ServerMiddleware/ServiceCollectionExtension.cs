using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Solidex.Microservices.Core.Infrasructure.Authorization;
using Swashbuckle.AspNetCore.Filters;

namespace Solidex.Microservices.Core.ServerMiddleware
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSolidAuthorization(this IServiceCollection collection)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims

            collection
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = AuthOptions.JwtIssuer,
                        ValidAudience = AuthOptions.JwtIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.JwtKey)),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });

            return collection;
        }

        public static IServiceCollection AddSwaggerConf(this IServiceCollection collection, string assemblyName)
        {
            collection.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v3", new OpenApiInfo {Title = $"{assemblyName} API", Version = "v3"});
                var basePath = AppContext.BaseDirectory;
                c.IncludeXmlComments(basePath + $"{assemblyName}.xml");

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            return collection;
        }
        
    }
}
