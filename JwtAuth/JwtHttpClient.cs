using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Solidex.Core.Base.ComplexTypes;

namespace Solidex.Microcervices.Core.JwtAuth
{
    public class JwtHttpClient: HttpClient
    {
        public JwtHttpClient(string hostUrl): base(new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
        })
        {
            Timeout = TimeSpan.FromMinutes(1);
            DefaultRequestHeaders.Add("Accept", "application/json");
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenProvider.GenerateSystemToken(SystemUsersEnumeration.SystemMessage));

            BaseAddress = new Uri(hostUrl);
        }
        public JwtHttpClient(string hostUrl, IHttpContextAccessor accessor): base(new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
        })
        {
            Timeout = TimeSpan.FromMinutes(1);
            DefaultRequestHeaders.Add("Accept", "application/json");

            try
            {
                DefaultRequestHeaders.Add("Authorization", new string[] { accessor.HttpContext.Request.Headers["Authorization"] });
            }
            catch (Exception e)
            {
                DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenProvider.GenerateSystemToken(SystemUsersEnumeration.SystemMessage));
            }

            BaseAddress = new Uri(hostUrl);
        }
        
    }
}