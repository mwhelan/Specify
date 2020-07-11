using System.Collections.Generic;
using FluentResults;

namespace ApiTemplate.Api.Domain.Common.FluentResult
{
    public class RecordsCreatedSuccess : Success
    {
        public List<int> NewIds { get; }

        public RecordsCreatedSuccess(int id) 
            : base("Record created")
        {
            NewIds = new List<int> {id};
        }

        public RecordsCreatedSuccess(List<int> newIds)
            : base("Records created")
        {
            NewIds = newIds;
        }
    }
}
