using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace Specs.Library.Drivers.Api
{
    public class ApiDriver
    {
        public ITestHost Server { get; }

        private object _modelResult;

        public ApiDriver(ITestHost server)
        {
            Server = server;
        }

        public HttpClient Client => Server.Client;

        public HttpResponseMessage Response { get; set; }

        public void Send(HttpRequestMessage request)
        {
            try
            {
                Response = Client.SendAsync(request).Result;
            }
            catch (AggregateException aggregateException)
            {
                foreach (var exception in aggregateException.InnerExceptions)
                {
                    throw exception;
                }
            }
        }

        public TModel Model<TModel>(JsonConverter converter = null)
        {
            if (_modelResult == null)
            {
                var content = Response.Content.ReadAsStringAsync().Result;

                _modelResult = (converter == null) ?
                    JsonConvert.DeserializeObject<TModel>(content) :
                    JsonConvert.DeserializeObject<TModel>(content, converter);
            }

            return (TModel)_modelResult;
        }

        public TResponse Send<TResponse>(HttpRequestMessage request)
        {
            Response = Client.SendAsync(request).Result;
            var content = Response.Content.ReadAsStringAsync().Result;
            var model = JsonConvert.DeserializeObject<TResponse>(content);
            return model;
        }
    }
}