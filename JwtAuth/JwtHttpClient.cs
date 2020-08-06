using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Solidex.Core.Base.ComplexTypes;

namespace Microcervices.Core.JwtAuth
{
    public class JwtHttpClient: HttpClient
    {
        public JwtHttpClient(string hostUrl, IHttpContextAccessor accessor): base(new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
        })
        {
            Timeout = TimeSpan.FromMinutes(1);
            DefaultRequestHeaders.Add("Accept", "application/json");

            if (accessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                DefaultRequestHeaders.Add("Authorization", new string[] { accessor.HttpContext.Request.Headers["Authorization"] });
            }
            else
            {
                DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenProvider.GenerateSystemToken(SystemUsersEnumeration.SystemMessage));
            }

            BaseAddress = new Uri(hostUrl);
        }
        
    }
}