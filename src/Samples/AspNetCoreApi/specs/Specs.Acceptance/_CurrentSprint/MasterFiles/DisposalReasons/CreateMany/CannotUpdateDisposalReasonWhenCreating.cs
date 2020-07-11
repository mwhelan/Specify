using System.Collections.Generic;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Contracts;
using ApiTemplate.Api.Contracts.Responses;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using FluentAssertions;
using Specs.Library.Builders.Entities.MasterFiles;
using Specs.Library.Drivers.Api;
using Specs.Library.Extensions;
using TestStack.Dossier.DataSources.Picking;

namespace Specs.Acceptance._CurrentSprint.MasterFiles.DisposalReasons.CreateMany
{
    public class CannotUpdateDisposalReasonWhenCreating : ScenarioFor<AsyncApiDriver, DisposalReasonStory>
    {
        private ApiResponse<RecordsCreatedResponse> _result;
        private List<DisposalReasonDto> _command;

        public void Given_some_new_Master_Files_have_Id_greater_than_zero()
        {
            var keys = new List<int> {0, 999, -2}; // 999 is an invalid key because it's not less than or equal to zero
            _command = DisposalReasonDtoBuilder.CreateListOfSize(3)
                .All()
                .Set(x => x.Id, Pick.RepeatingSequenceFrom(keys).Next)
                .Set(x => x.Reason, string.Empty);
        }

        public async Task When_I_attempt_to_create_it()
        {
            _result = await SUT.PostAsync<RecordsCreatedResponse>(ApiRoutes.Master.CreateFor<DisposalReason>(), _command);
        }

        public void Then_the_new_file_should_not_be_created()
        {
            Db.Set<DisposalReason>().Should().HaveCount(0);
        }

        public void And_then_I_should_be_told_the_reasons_why_each_update_with_each_row_identified_by_key()
        {
            _result.ShouldContainErrors(
                "Id must be less than or equal to zero. Use the Update endpoint for updating existing records.");
        }
    }
}