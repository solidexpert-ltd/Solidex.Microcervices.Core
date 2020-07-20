using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Microcervices.Core
{
  public static class ServiceCollectionExtension
  {

    public static string JwtIssuer = "solid.expert";
    public static string JwtKey = "7746808a-93a6-4a13-b63a-cfc0fb1c8c34";

    public static IServiceCollection UseSolidAuthorization(this IServiceCollection collection)
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
            ValidIssuer = JwtIssuer,
            ValidAudience = JwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey)),
            ClockSkew = TimeSpan.Zero // remove delay of token when expire
          };
         
        });

      return collection;
    }

    public static IServiceCollection UseSwaggerConf(this IServiceCollection collection, string assemblyName)
    {
      collection.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = $"{assemblyName} API", Version = "v1" });
        var basePath = AppContext.BaseDirectory;
        c.IncludeXmlComments(basePath + $"{assemblyName}.xml");

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
          Name = "Authorization",
          In = ParameterLocation.Header,
        });
        c.OperationFilter<SecurityRequirementsOperationFilter>();
      });

      return collection;
    }
  }
}
