using System.Collections.Generic;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Common.Extensions;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Contracts;
using ApiTemplate.Api.Contracts.Responses;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using FluentAssertions;
using Specs.Library.Builders.Entities.MasterFiles;
using Specs.Library.Drivers.Api;
using Specs.Library.Extensions;

namespace Specs.Acceptance._CurrentSprint.MasterFiles.DisposalReasons.CreateMany
{
    public class CreateSingleDisposalReasonWithInvalidData : ScenarioFor<AsyncApiDriver, DisposalReasonStory>
    {
        private ApiResponse<RecordsCreatedResponse> _result;
        private List<DisposalReasonDto> _command;

        public void Given_an_invalid_new_Master_File()
        {
            DisposalReasonDto dto = new DisposalReasonDtoBuilder()
                .Set(x => x.Id, 0)
                .Set(x => x.Reason, string.Empty);
            _command = dto.ToCollection();
        }

        public async Task When_I_attempt_to_create_it()
        {
            _result = await SUT.PostAsync<RecordsCreatedResponse>(ApiRoutes.Master.CreateFor<DisposalReason>(), _command);
        }

        public void Then_the_new_file_should_not_be_created()
        {
            Db.Set<DisposalReason>().Should().HaveCount(0);
        }

        public void And_then_I_should_be_told_the_reasons_why()
        {
            _result.ShouldContainErrors("'Reason' must not be empty.");
        }
    }
}