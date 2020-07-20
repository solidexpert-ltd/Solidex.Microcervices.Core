using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microcervices.Core.JwtAuth;
using Microservices.RestClient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Refit;

namespace Microcervices.Core.HttpManager
{
    public static class RefitService
    {
        public static T CreateSystemUserService<T>(ServerUrls url, IWebHostEnvironment env)
        {
            var token = JwtTokenProvider.GenerateSystemToken();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.BaseAddress = new Uri(env.IsDevelopment() ? url.TestUrl : url.DeployUrl);

            return RestService.For<T>(client);
        }
    }
}