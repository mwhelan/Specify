using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Contracts;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using FluentAssertions;
using Specs.Library.Builders.Entities.MasterFiles;
using Specs.Library.Data;
using Specs.Library.Drivers.Api;

namespace Specs.Acceptance._CurrentSprint.MasterFiles.DisposalReasons.UpdateMany
{
    public class UpdateSingleDisposalReasonWithValidData : ScenarioFor<AsyncApiDriver, DisposalReasonStory>
    {
        private ApiResponse _result;
        private DisposalReason _existingItem;
        private List<DisposalReasonDto> _updates;

        public void Given_I_have_made_valid_changes_to_one_existing_item()
        {
            _existingItem = new DisposalReasonBuilder().Persist();

            DisposalReasonDto item = new DisposalReasonDtoBuilder()
               .MapFromEntity(_existingItem)
               .Set(x => x.DisposalReasonDescription, "Updated");

           _updates = new List<DisposalReasonDto>{item};
        }

        public async Task When_I_attempt_to_apply_the_changes()
        {
            _result = await SUT.PutAsync(ApiRoutes.Master.UpdateFor<DisposalReason>(), _updates);
        }

        public void Then_the_changes_should_be_saved()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.OK);
            Db.Find<DisposalReason>(_existingItem.Id)
                .DisposalReasonDescription.Should().Be("Updated");
        }
    }
}