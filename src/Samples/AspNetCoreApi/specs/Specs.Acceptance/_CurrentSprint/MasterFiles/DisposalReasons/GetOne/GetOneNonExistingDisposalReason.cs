using System.Net;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Contracts;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using FluentAssertions;
using Specs.Library.Drivers.Api;

namespace Specs.Acceptance._CurrentSprint.MasterFiles.DisposalReasons.GetOne
{
    public class GetOneNonExistingDisposalReason : ScenarioFor<AsyncApiDriver, DisposalReasonStory>
    {
        private ApiResponse<DisposalReasonDto> _result;

        public async Task When_I_attempt_to_view_a_master_file_that_does_not_exist()
        {
            _result = await SUT.GetAsync<DisposalReasonDto>(ApiRoutes.Master.GetFor<DisposalReason>(99));
        }

        public void Then_I_should_be_warned_that_it_does_not_exist()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}