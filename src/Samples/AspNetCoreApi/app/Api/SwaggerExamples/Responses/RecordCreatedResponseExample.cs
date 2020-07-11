using ApiTemplate.Api.Contracts.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace ApiTemplate.Api.SwaggerExamples.Responses
{
    public class RecordsCreatedResponseExample : IExamplesProvider<RecordsCreatedResponse>
    {
        public RecordsCreatedResponse GetExamples()
        {
            return new RecordsCreatedResponse(39);
        }
    }
}