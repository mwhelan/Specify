using System.Collections.Generic;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Contracts;
using ApiTemplate.Api.Contracts.Responses;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using FluentAssertions;
using Specs.Library.Builders.Entities.MasterFiles;
using Specs.Library.Drivers.Api;
using TestStack.Dossier.DataSources.Picking;

namespace Specs.Acceptance._CurrentSprint.MasterFiles.DisposalReasons.CreateMany
{
    public class CreateMultipleDisposalReasonsWithInvalidData : ScenarioFor<AsyncApiDriver, DisposalReasonStory>
    {
        private ApiResponse<RecordsCreatedResponse> _result;
        private List<DisposalReasonDto> _command;

        public void Given_an_invalid_list_of_new_Master_Files()
        {
            var keys = new List<int> {0, -1, -2};
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
             _result.Errors.Should().BeEquivalentTo(new List<ErrorModel>()
            {
                new ErrorModel("Reason", "'Reason' must not be empty.", 0),
                new ErrorModel("Reason", "'Reason' must not be empty.", -1),
                new ErrorModel("Reason", "'Reason' must not be empty.", -2)
            });
        }
    }
}