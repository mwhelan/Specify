using System.Collections.Generic;
using FluentResults;

namespace ApiTemplate.Api.Domain.Common.FluentResult
{
    public class Warning : Reason
    {
        public Warning(string message)
        {
            Message = message;
        }

        public Warning WithMetadata(string metadataName, object metadataValue)
        {
            Metadata.Add(metadataName, metadataValue);
            return this;
        }

        public Warning WithMetadata(Dictionary<string, object> metadata)
        {
            foreach (var metadataItem in metadata)
            {
                Metadata.Add(metadataItem.Key, metadataItem.Value);
            }

            return this;
        }
    }
}