using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace ApiTemplate.Api.Infrastructure.HealthChecks
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void ConfigureHealthChecks(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions()
            {
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                },
                ResponseWriter = WriteHealthCheckReadyResponse,
                Predicate = (check) => check.Tags.Contains("ready"),
                AllowCachingResponses = false
            });

            endpoints.MapHealthChecks("/health/live", new HealthCheckOptions()
            {
                Predicate = (check) => !check.Tags.Contains("ready"),
                ResponseWriter = WriteHealthCheckLiveResponse,
                AllowCachingResponses = false
            });
        }

        private static Task WriteHealthCheckReadyResponse(HttpContext httpContext, HealthReport report)
        {
            httpContext.Response.ContentType = "application/json";

            var healthCheck = ReadyHealthCheck.Create(report);
            var json = JsonConvert.SerializeObject(healthCheck);

            return httpContext.Response.WriteAsync(json);
        }

        private static Task WriteHealthCheckLiveResponse(HttpContext httpContext, HealthReport report)
        {
            httpContext.Response.ContentType = "application/json";

            var healthCheck = ReadyHealthCheck.Create(report);
            var json = JsonConvert.SerializeObject(healthCheck);

            return httpContext.Response.WriteAsync(json);
        }
    }
}