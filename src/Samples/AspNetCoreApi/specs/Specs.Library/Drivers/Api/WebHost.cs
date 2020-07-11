using System;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Specs.Library.Drivers.Api
{
    public class WebHost : ITestHost
    {
        private HttpClient _client;

        public Uri BaseAddress => new Uri("https://localhost:5001");
        public string AppName => "WebHost - ApiTemplate.Api";

        public HttpClient Client => _client ?? (_client = GetClient());

        public void Start()
        {
        }

        public void Stop()
        {
        }

        private HttpClient GetClient()
        {
            var client = new HttpClient(new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                MaxConnectionsPerServer = int.MaxValue,
                UseCookies = false,
                ServerCertificateCustomValidationCallback = ValidateLocalhostCertificate
            }) {BaseAddress = BaseAddress};


            return client;
        } 

        private static bool ValidateLocalhostCertificate(HttpRequestMessage arg1, X509Certificate2 arg2, X509Chain arg3, SslPolicyErrors arg4)
        {
            return true;
            //if (arg1.RequestUri.Host == "127.0.0.1")
            //{
            //    return true;
            //}
            //else
            //{
            //    // default validation
            //}
        }
    }
}