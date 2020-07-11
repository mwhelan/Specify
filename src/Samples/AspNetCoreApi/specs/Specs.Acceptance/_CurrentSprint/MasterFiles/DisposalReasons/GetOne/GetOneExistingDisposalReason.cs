using System.Net;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Contracts;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using FluentAssertions;
using Specs.Library.Builders.Entities.MasterFiles;
using Specs.Library.Data;
using Specs.Library.Drivers.Api;

namespace Specs.Acceptance._CurrentSprint.MasterFiles.DisposalReasons.GetOne
{
    public class GetOneExistingDisposalReason : ScenarioFor<AsyncApiDriver, DisposalReasonStory>
    {
        private ApiResponse<DisposalReasonDto> _result;
        private DisposalReason _existing;

        public void Given_I_have_created_a_master_file()
        {
            _existing = new DisposalReasonBuilder().Persist();
        }

        public async Task When_I_attempt_to_view_it()
        {
             _result = await SUT.GetAsync<DisposalReasonDto>(ApiRoutes.Master.GetFor<DisposalReason>(_existing.Id));
        }

        public void Then_I_should_see_all_the_files()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.OK);
            _result.Model.Id.Should().Be(_existing.Id);
        }
    }
}
