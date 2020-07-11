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
using TestStack.Dossier.Lists;

namespace Specs.Acceptance._CurrentSprint.MasterFiles.DisposalReasons.UpdateMany
{
    public class UpdateCannotCreateDisposalReason : ScenarioFor<AsyncApiDriver, DisposalReasonStory>
    {
        private ApiResponse _result;
        private List<DisposalReason> _existingItems;
        private List<DisposalReasonDto> _updates;

        public void Given_I_am_updating_Master_Files_and_one_has_Id_less_than_zero()
        {
            _existingItems = DisposalReasonBuilder.CreateListOfSize(3).Persist().ToList();
            _updates = DisposalReasonDtoBuilder.CreateListFrom(_existingItems)
                .All()
                .Set(x => x.Reason, string.Empty)
                .TheLast(1)
                .Set(x => x.Id, -1);
        }

        public async Task When_I_attempt_to_apply_the_changes()
        {
            _result = await SUT.PutAsync(ApiRoutes.Master.UpdateFor<DisposalReason>(), _updates);
        }

        public void Then_the_changes_should_not_be_saved()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            foreach (var entity in Db.Set<DisposalReason>())
            {
                entity.Reason.Should().Be(_existingItems.Single(x => x.Id == entity.Id).Reason);
            }
        }

        public void And_then_I_should_be_told_the_reasons_why()
        {
            _result.Errors[0].Message.Should().Be("Id must be greater than zero. Use the Create endpoint for new records.");
            _result.Errors.Count.Should().Be(1);
        }
    }
}