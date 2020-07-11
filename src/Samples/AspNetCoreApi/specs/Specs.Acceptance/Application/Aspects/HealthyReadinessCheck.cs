using System.Net;
using System.Threading.Tasks;
using ApiTemplate.Api.Infrastructure.HealthChecks;
using FluentAssertions;
using Specs.Library.Drivers.Api;

namespace Specs.Acceptance.Application.Aspects
{
    public class HealthyReadinessCheck : ScenarioFor<AsyncApiDriver, DevOpsStory>
    {
        private ApiResponse<ReadyHealthCheck> _result;

        public void Given_the_database_is_running()
        {
        }

        public async Task When_I_check_the_readiness()
        {
            _result = await SUT.GetAsync<ReadyHealthCheck>("health/ready");
        }
        
        public void Then_the_status_should_be_healthy()
        {
            _result.Model.OverallStatus.Should().Be("Healthy");
        }

        public void AndThen_the_status_code_should_be_Ok()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}