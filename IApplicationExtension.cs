using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using Swashbuckle.Swagger;

namespace Microcervices.Core
{
    public static class IApplicationExtension
    {
        public static IApplicationBuilder UseSwaggerSolidConf(this IApplicationBuilder app, bool env, string basepath)
        {
            if (env)
            {
                app.UseSwagger();
                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            }
            else
            {
                app.UseSwagger(c =>
                {
                    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    {
                        swaggerDoc.Servers = new List<OpenApiServer>
                            {new OpenApiServer {Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{basepath}"}};
                    });

                    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    {
                        OpenApiPaths paths = new OpenApiPaths();
                        foreach (var path in swaggerDoc.Paths)
                        {
                            paths.Add(path.Key.Replace(basepath, "/"), path.Value);
                        }

                        swaggerDoc.Paths = paths;
                    });
                });
            }

            return app;
        }
    }
}
