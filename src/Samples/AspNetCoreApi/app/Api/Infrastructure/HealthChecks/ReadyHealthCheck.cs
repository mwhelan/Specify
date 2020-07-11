using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ApiTemplate.Api.Infrastructure.HealthChecks
{
    public class ReadyHealthCheck
    {
        public string OverallStatus { get; set; }

        public string TotalChecksDuration { get; set; }

        public List<DependencyHealthCheck> DependencyHealthChecks { get; set; } = new List<DependencyHealthCheck>();

        public static ReadyHealthCheck Create(HealthReport result)
        {
            return new ReadyHealthCheck
            {
                OverallStatus = result.Status.ToString(),
                TotalChecksDuration = result.TotalDuration.TotalSeconds.ToString("0:0.00"),
                DependencyHealthChecks = Enumerable.ToList<DependencyHealthCheck>(result.Entries
                        .Select(entry => new DependencyHealthCheck
                        {
                            Dependency = entry.Key,
                            Status = entry.Value.Status.ToString(),
                            Duration = entry.Value.Duration.TotalSeconds.ToString("0:0.00"),
                            Exception = entry.Value.Exception,
                            Data = entry.Value.Data
                        }))
            };
        }
    }
}
