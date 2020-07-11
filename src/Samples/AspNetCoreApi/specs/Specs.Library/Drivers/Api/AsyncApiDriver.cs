using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Common.Paging;

namespace Specs.Library.Drivers.Api
{
    public class AsyncApiDriver
    {
        public ITestHost Server { get; }

        public AsyncApiDriver(ITestHost server)
        {
            Server = server;
        }

        public HttpClient Client => Server.Client;

        public async Task<ApiResponse<TResponse>> GetAsync<TResponse>(string requestUri)
        {
            // var request = Get.MotherFor.HttpRequestMessage.Get(requestUri);
            var response = await Client.GetAsync(requestUri);

            return new ApiResponse<TResponse>(response);
        }

        public async Task<ApiResponse<TResponse>> GetWithCheckAsync<TResponse>(string requestUri, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var response = await GetAsync<TResponse>(requestUri);

            if (response.StatusCode != expectedStatusCode)
            {
                throw new ApiDriverException($"{nameof(GetWithCheckAsync)} received '{response.StatusCode}' but expected '{expectedStatusCode}'");
            }

            return response;
        }

        public async Task<ApiResponse<PagedList<TResponse>>> GetAllPagedAsync<TResponse>(string requestUri)
        {
            var response = await Client.GetAsync(requestUri);

            return new ApiResponse<PagedList<TResponse>>(response);
        }

        public async Task<ApiResponse<List<TResponse>>> GetAllAsync<TResponse>(string requestUri)
        {
            var response = await Client.GetAsync(requestUri);

            return new ApiResponse<List<TResponse>>(response);
        }

        public async Task<ApiResponse<List<TResponse>>> GetAllWithCheckAsync<TResponse>(string requestUri, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var response = await GetAllAsync<TResponse>(requestUri);

            if (response.StatusCode != expectedStatusCode)
            {
                throw new ApiDriverException($"{nameof(GetAllWithCheckAsync)} received '{response.StatusCode}' but expected '{expectedStatusCode}'");
            }

            return response;
        }

        public async Task<ApiResponse<TResponse>> PostAsync<TResponse>(string requestUri, object resource)
        {
            var content = resource.AsJsonContent();
            var response = await Client.PostAsync(requestUri, content);
            return new ApiResponse<TResponse>(response);
        }

        public async Task<ApiResponse<TResponse>> PostWithCheckAsync<TResponse>(string requestUri, object resource, HttpStatusCode expectedStatusCode = HttpStatusCode.Created)
        {
            var content = resource.AsJsonContent();
            var response = await Client.PostAsync(requestUri, content);

            if (response.StatusCode != expectedStatusCode)
            {
                throw new ApiDriverException($"{nameof(PostAsync)} received '{response.StatusCode}' but expected '{expectedStatusCode}'");
            }

            return new ApiResponse<TResponse>(response);
        }

        public async Task<ApiResponse> PostAsync(string requestUri, object resource)
        {
            var content = resource.AsJsonContent();
            var response = await Client.PostAsync(requestUri, content);
            return new ApiResponse(response);
        }

        public async Task<ApiResponse<TResponse>> PutAsync<TResponse>(string requestUri, object resource)
        {
            var content = resource.AsJsonContent();
            var response = await Client.PutAsync(requestUri, content);
            return new ApiResponse<TResponse>(response);
        }

        public async Task<ApiResponse<TResponse>> PutWithCheckAsync<TResponse>(string requestUri, object resource, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var response = await PutAsync<TResponse>(requestUri, resource);

            if (response.StatusCode != expectedStatusCode)
            {
                throw new ApiDriverException($"{nameof(PutWithCheckAsync)} received '{response.StatusCode}' but expected '{expectedStatusCode}'");
            }

            return response;
        }

        public async Task<ApiResponse> PutAsync(string requestUri, object resource)
        {
            var content = resource.AsJsonContent();
            var response = await Client.PutAsync(requestUri, content);
            return new ApiResponse(response);
        }

        public async Task<ApiResponse> DeleteAsync(string requestUri)
        {
            var response = await Client.DeleteAsync(requestUri);
            return new ApiResponse(response);
        }

        public async Task<ApiResponse> DeleteWithCheckAsync(string requestUri, HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent)
        {
            var response = await DeleteAsync(requestUri);

            if (response.StatusCode != expectedStatusCode)
            {
                throw new ApiDriverException($"{nameof(DeleteWithCheckAsync)} received '{response.StatusCode}' but expected '{expectedStatusCode}'");
            }

            return response;
        }

        public async Task<ApiResponse<TResponse>> SendAsync<TResponse>(HttpRequestMessage request)
        {
            var response = await Client.SendAsync(request);
            return new ApiResponse<TResponse>(response);
        }

        public async Task<ApiResponse<TResponse>> SendWithCheckAsync<TResponse>(HttpRequestMessage request, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var response = await SendAsync<TResponse>(request);

            if (response.StatusCode != expectedStatusCode)
            {
                throw new ApiDriverException($"{nameof(SendWithCheckAsync)} received '{response.StatusCode}' but expected '{expectedStatusCode}'");
            }

            return response;
        }

        public async Task<ApiResponse> SendAsync(HttpRequestMessage request)
        {
            var response = await Client.SendAsync(request);
            return new ApiResponse(response);
        }

        public async Task<ApiResponse> SendWithCheckAsync(HttpRequestMessage request, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var response = await SendAsync(request);

            if (response.StatusCode != expectedStatusCode)
            {
                throw new ApiDriverException($"{nameof(SendWithCheckAsync)} received '{response.StatusCode}' but expected '{expectedStatusCode}'");
            }

            return response;
        }
    }
}