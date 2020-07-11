using System.Collections.Generic;
using System.Linq;
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
    public class UpdateMultipleDisposalReasonsWithValidData : ScenarioFor<AsyncApiDriver, DisposalReasonStory>
    {
        private ApiResponse _result;
        private List<DisposalReason> _existingItems;
        private List<DisposalReasonDto> _updates;

        public void Given_I_have_made_valid_changes_to_multiple_existing_items()
        {
            _existingItems = DisposalReasonBuilder.CreateListOfSize(3).Persist().ToList();
            _updates = DisposalReasonDtoBuilder.CreateListFrom(_existingItems)
                    .All().Set(x => x.DisposalReasonDescription, "Updated");
        }

        public async Task When_I_attempt_to_apply_the_changes()
        {
            _result = await SUT.PutAsync(ApiRoutes.Master.UpdateFor<DisposalReason>(), _updates);
        }

        public void Then_the_changes_should_be_saved()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.OK);

            foreach (var entity in Db.Set<DisposalReason>())
            {
                entity.DisposalReasonDescription.Should().Be("Updated");
            }
        }
    }
}