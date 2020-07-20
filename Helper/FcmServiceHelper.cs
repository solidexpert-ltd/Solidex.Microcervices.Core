using FcmSharp.Serializer;
using FcmSharp.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Microcervices.Core.Helper
{
    public static class FcmServiceHelper
    {
        public class FcmSettings : IFcmClientSettings
        {
            public string FcmUrl => "https://fcm.googleapis.com/fcm/send";
            public string ApiKey => "AAAAskLPlhA:APA91bFNiZlQAOjxKp7KGPh2VcMrANOslJ-Zp8I3jB0Dgm6JjYzJ9f0n7mJIFIZXYOlrD0CRfvN1HlBi1-R9MW6Zq2FIOVgt5vPaH_nIzjw2VprKmnD-McWT34yncNghLqhmol1spb2K";
        }

        public class MyFcmJsonSerializer : IJsonSerializer
        {
            public string SerializeObject(object value)
            {
                var str = JsonConvert.SerializeObject(value, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                return str;
            }

            public TTargetType DeserializeObject<TTargetType>(string value)
            {
                return JsonConvert.DeserializeObject<TTargetType>(value);
            }
        }
    }
}