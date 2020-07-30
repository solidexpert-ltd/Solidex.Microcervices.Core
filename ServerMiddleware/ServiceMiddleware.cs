using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace Microcervices.Core.ServerMiddleware
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

    }
}
