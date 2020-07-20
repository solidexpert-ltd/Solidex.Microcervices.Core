using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Solidex.Core.ViewModels.UserInformation;

namespace Microcervices.Core.HttpManager
{
    public class HttpManager: IDisposable
    {
        private const string DevelopUrl = "https://solidexcrm.com/";
        private const string ServerUrl = "http://192.168.2.101/";

        public bool IsDevelop { get; }

        private HttpClient HttpClient { get; }

        public HttpManager(bool develop)
        {
            IsDevelop = develop;

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            HttpClient = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromMinutes(1)
            };
        }

        public void Dispose()
        {
            HttpClient.Dispose();
        }

        public void SetReqestHeaders(IHeaderDictionary headers)
        {
            HttpClient.DefaultRequestHeaders.Clear();

            foreach (var header in headers)
            {
                if (header.Key == "Accept" || header.Key == "Authorization")
                {
                    HttpClient.DefaultRequestHeaders.Add(header.Key, header.Value.ToString());
                }
            }
        }

        public async Task<UserInformationViewModel.UserInformationViewModelParticipant> GetParticipantAsync(string shortcut, Guid id)
        {
            var model = await GetMethodAsync<UserInformationViewModel.UserInformationViewModelParticipant>(
                $"api/v2/route/{shortcut}/participant?participantId={id}");
            return model;
        }

        protected async Task<TModel> GetMethodAsync<TModel>(string url) where TModel: class
        {
            string s = $"{(IsDevelop ? DevelopUrl : ServerUrl)}{url}";

            var response = await HttpClient.GetAsync(requestUri: Uri.EscapeUriString(s));

            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();

            TModel model = JsonConvert.DeserializeObject<TModel>(json);

            return model;
        }
    }
}
