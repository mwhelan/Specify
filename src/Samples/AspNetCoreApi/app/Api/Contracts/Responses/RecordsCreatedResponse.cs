using System.Collections.Generic;
using ApiTemplate.Api.Domain.Common.FluentResult;

namespace ApiTemplate.Api.Contracts.Responses
{
    public class RecordsCreatedResponse
    {
        public RecordsCreatedResponse() { }

        public RecordsCreatedResponse(RecordsCreatedSuccess reason)
        {
            NewIds = reason.NewIds;
            Message = reason.Message;
        }

        public RecordsCreatedResponse(int newId)
        {
            NewIds.Add(newId);
            Message = $"Record created with Id {newId}";
        }

        public RecordsCreatedResponse(List<int> newIds)
        {
            NewIds = newIds;
            Message = "Records created";
        }

        public List<int> NewIds { get; set; } = new List<int>();
        public string Message { get; set; }
    }
}