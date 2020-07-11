using System;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;

namespace Specs.Library.Drivers.Api
{
    public class MemoryHost : ITestHost
    {
        private readonly TestServer _server;
        private HttpClient _client;

        public MemoryHost(TestServer server)
        {
            _server = server;
        }

        public Uri BaseAddress => new Uri("http://localhost/");

        public string AppName => "Memory Host - ApiTemplate.Api";

        // Client is singleton: https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
        public HttpClient Client => _client ??= CreateHttpClient();

        public void Start()
        {
            // TestServer already created
        }

        public void Stop()
        {
            try
            {
                _server.Dispose();
                _client.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not stop server: {0}", e);
            }
        }

        private HttpClient CreateHttpClient()
        {
            return _server.CreateClient();
        }
    }
}