using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using ApiTemplate.Api.Contracts.Responses;
using Newtonsoft.Json;

namespace Specs.Library.Drivers.Api
{
    public class ApiResponse<T> : ApiResponse
    {
         public T Model { get; private set; }

        public ApiResponse(HttpResponseMessage message)
            : base(message)
        {
            GetModel();
        }

        public ApiResponse(HttpStatusCode statusCode, string body)
            : this(Build(statusCode, body)) { }

        private void GetModel()
        {
            try
            {
                if (!ResponseMessage.IsSuccessStatusCode) return;

                // All application endpoints should be wrapped with SuccessResponse by convention
                var model = JsonConvert.DeserializeObject<SuccessResponse<T>>(Body);
                Model = model.Data;
                Message = model.Message;

                // Some external endpoints, such as health checks, won't use SuccessResponse
                if (model.Data == null)
                {
                    Model = JsonConvert.DeserializeObject<T>(Body);
                }
            }
            catch (Exception exception)
            {
                throw new ApiDriverException($"Failed to deserialize body '{Body}' to type '{typeof(T)}'", exception);
            }
        }

        private static HttpResponseMessage Build(HttpStatusCode statusCode, string body)
        {
            return new HttpResponseMessage(statusCode)
            {
                Content = body.AsJsonContent()
            };
        }
    }

    public class ApiResponse
    {
        public ApiResponse(HttpResponseMessage response)
        {
            StatusCode = response.StatusCode;
            Body = response.Content.ReadAsStringAsync().Result;
            ResponseMessage = response;
            RequestMessage = response.RequestMessage;
            Headers = response.Headers;

            GetErrors();
        }

        public HttpStatusCode StatusCode { get; protected set; }

        public string Body { get; protected set; }

        public virtual bool Success => StatusCode == HttpStatusCode.OK;

        public HttpResponseMessage ResponseMessage { get; protected set; }
        public HttpRequestMessage RequestMessage { get; set; }
        public HttpResponseHeaders Headers { get; set; }

        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
        public string Message { get; set; }

        private void GetErrors()
        {
            try
            {
                if (ResponseMessage.IsSuccessStatusCode) return;

                var error = JsonConvert.DeserializeObject<ErrorResponse>(Body);
                Errors = error.Errors;
                Message = error.Message;
            }
            catch (Exception exception)
            {
                throw new ApiDriverException($"Failed to deserialize errors to type '{typeof(ErrorResponse)}'", exception);
            }
        }

        public List<ErrorModel> ErrorsForProperty(string propertyName)
        {
            return Errors.Where(x => x.PropertyName == propertyName).ToList();
        }
    }
}
