using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microcervices.Core.Infrasructure.Attributes;
using Microcervices.Core.Infrasructure.RestApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Microcervices.Core.ServerMiddleware
{
    public class ApiProtector
    {
        private readonly RequestDelegate _next;

        public ApiProtector(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IWebHostEnvironment env)
        {
            if (context.User.IsInRole("root-admin") || context.User.IsInRole("root-user"))
            {
                await _next.Invoke(context);
                return;
            }

            var endpoint = context.GetEndpoint();

            if (endpoint == null)
            {
                await _next.Invoke(context);
                return;
            }

            try
            {
                var meta = endpoint.Metadata.GetMetadata<ApiProtectAttribute>();

                if (meta == null)
                {
                    await _next.Invoke(context);
                    return;
                }

                var userid = context.User.Claims.GetUserInformationId();

                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                };

                var client = new HttpClient(handler)
                {
                    Timeout = TimeSpan.FromMinutes(1)
                };

                foreach (var header in context.Request.Headers)
                {
                    if (header.Key == "Accept" || header.Key == "Authorization")
                        client.DefaultRequestHeaders.Add(header.Key, header.Value.ToString());
                }

                client.BaseAddress = new Uri(env.IsDevelopment() ? ServerUrls.CompanyApi.TestUrl : ServerUrls.CompanyApi.DeployUrl);

                var api = RestService.For<ICompanyApi>(client, new RefitSettings
                {
                    ContentSerializer = new NewtonsoftJsonContentSerializer(
                        new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        }
                    )
                });

                var shortcut = (string) context.Request.RouteValues.FirstOrDefault(f => f.Key == "shortcut").Value;

                var participants = await api.GetParticipantsAsync(shortcut, new FilterRequest());

                if (!participants.Elements.Any(a => a.UserInformationID == userid))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Not authorize in this company");
                }

                await _next.Invoke(context);
            }
            catch (Exception)
            {
                await _next.Invoke(context);
            }

        }
    }
}