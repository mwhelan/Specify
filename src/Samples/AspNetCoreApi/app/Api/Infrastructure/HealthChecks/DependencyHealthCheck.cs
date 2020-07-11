using System;
using System.Collections.Generic;

namespace ApiTemplate.Api.Infrastructure.HealthChecks
{
    public class DependencyHealthCheck
    {
        public string Dependency { get; set; }
        public string Status { get; set; }

        public string Duration { get; set; }

        public Exception Exception { get; set; }

        public IReadOnlyDictionary<string, object> Data { get; set; }
    }
}