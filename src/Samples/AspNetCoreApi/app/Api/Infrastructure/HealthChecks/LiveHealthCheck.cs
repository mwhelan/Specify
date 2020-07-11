using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ApiTemplate.Api.Infrastructure.HealthChecks
{
    public class LiveHealthCheck
    {
        public string OverallStatus { get; set; }

        public string TotalChecksDuration { get; set; }

        public static LiveHealthCheck Create(HealthReport result)
        {
            return new LiveHealthCheck
            {
                OverallStatus = result.Status.ToString(),
                TotalChecksDuration = result.TotalDuration.TotalSeconds.ToString("0:0.00"),
            };
        }
    }
}