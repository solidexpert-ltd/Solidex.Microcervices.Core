using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;


namespace Solidex.Microcervices.Core.ServerMiddleware
{
    public static class ServiceMiddleware
    {
        
        public static void AddServerControllers(this IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.Formatting = Formatting.Indented;
                    opt.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    opt.SerializerSettings.DateParseHandling = DateParseHandling.DateTime;
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    opt.UseCamelCasing(true);
                });
        }

        public static Guid GetUserInformationId(this IEnumerable<Claim> claims)
        {
            var sod = claims.SingleOrDefault(s => s.Type == "UserInformationID");

            if ( sod == null) throw new UnauthorizedAccessException();

            return Guid.Parse(sod.Value);
        }



        public static bool IsInRootAdmin(this ControllerBase controller)
        {
            return controller.User.IsInRole("root-admin");
        }

        public static string GetMicroserviceHost(this IConfiguration configuration, string name)
        {
            return configuration?.GetSection("ServicesHosts")?[name];
        }

    }
}
