using System.Collections.Generic;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Common.Extensions;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Contracts;
using ApiTemplate.Api.Contracts.Responses;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using FluentAssertions;
using Specify;
using Specs.Library.Builders.Entities.MasterFiles;
using Specs.Library.Data;
using Specs.Library.Drivers.Api;
using Specs.Library.Extensions;

namespace Specs.Acceptance._CurrentSprint.MasterFiles.DisposalReasons.CreateMany
{
    public class EachDisposalReasonHasUniqueReason : ScenarioFor<AsyncApiDriver, DisposalReasonStory>
    {
        private ApiResponse<RecordsCreatedResponse> _result;
        private List<DisposalReasonDto> _command;

        private DisposalReason _existing;

        public void Given_I_have_an_existing_master_file()
        {
            _existing = new DisposalReasonBuilder().Persist();
        }

        public void AndGiven_I_am_trying_to_create_new_Master_File_with_a_duplicate_value_for_the_existing_index()
        {
            DisposalReasonDto dto = _existing.MapTo(new DisposalReasonDto())
                .With(x => x.Id = 0);
            _command = dto.ToCollection();
        }

        public async Task When_I_attempt_to_create_it()
        {
            _result = await SUT.PostAsync<RecordsCreatedResponse>(ApiRoutes.Master.CreateFor<DisposalReason>(), _command);
        }

        public void Then_the_new_file_should_not_be_created()
        {
            Db.Set<DisposalReason>().Should().HaveCount(1);
        }

        public void And_then_I_should_be_told_the_reasons_why()
        {
            _result.ShouldHaveError("DisposalReason", "Unique constraint violation");
        }
    }
}