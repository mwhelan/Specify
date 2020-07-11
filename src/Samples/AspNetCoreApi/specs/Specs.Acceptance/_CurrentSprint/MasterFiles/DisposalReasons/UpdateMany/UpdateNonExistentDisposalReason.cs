using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Contracts;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using FluentAssertions;
using Specs.Library.Builders;
using Specs.Library.Drivers.Api;

namespace Specs.Acceptance._CurrentSprint.MasterFiles.DisposalReasons.UpdateMany
{
    public class UpdateNonExistentDisposalReason : ScenarioFor<AsyncApiDriver, DisposalReasonStory>
    {
        private ApiResponse _result;
        private List<DisposalReasonDto> _updates;

        public void Given_I_am_trying_to_edit_an_item_that_does_not_exist()
        {
            _updates = new List<DisposalReasonDto> {Get.InstanceOf<DisposalReasonDto>()};
        }

        public async Task When_I_attempt_to_apply_changes_to_it()
        {
            _result = await SUT.PutAsync(ApiRoutes.Master.UpdateFor<DisposalReason>(), _updates);
        }

        public void Then_I_should_be_warned_that_the_item_does_not_exist()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}