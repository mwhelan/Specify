using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Common.Extensions;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Contracts;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using FluentAssertions;
using Specs.Library.Builders.Entities.MasterFiles;
using Specs.Library.Data;
using Specs.Library.Drivers.Api;
using Specs.Library.Extensions;

namespace Specs.Acceptance._CurrentSprint.MasterFiles.DisposalReasons.UpdateMany
{
    public class UpdateSingleDisposalReasonWithInvalidData : ScenarioFor<AsyncApiDriver, DisposalReasonStory>
    {
        private ApiResponse _result;
        private DisposalReason _existingItem;
        private List<DisposalReasonDto> _updates;

        public void Given_I_have_made_invalid_changes_to_an_existing_item()
        {
            _existingItem = new DisposalReasonBuilder().Persist();
            _updates = new DisposalReasonDtoBuilder()
                .MapFromEntity(_existingItem)
                .Set(x => x.Reason, string.Empty)
                .Build()
                .ToCollection();
        }

        public async Task When_I_attempt_to_apply_the_changes()
        {
            _result = await SUT.PutAsync(ApiRoutes.Master.UpdateFor<DisposalReason>(), _updates);
        }

        public void Then_the_changes_should_not_be_saved()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            Db.Find<DisposalReason>(_existingItem.Id)
                .Reason.Should().Be(_existingItem.Reason);
        }

        public void And_then_I_should_be_told_the_reasons_why()
        {
            _result.ShouldContainErrors("'Reason' must not be empty.");
        }
    }
}