using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Common.Extensions;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Contracts;
using ApiTemplate.Api.Contracts.Responses;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using FluentAssertions;
using Specs.Library.Builders.Entities.MasterFiles;
using Specs.Library.Drivers.Api;

namespace Specs.Acceptance._CurrentSprint.MasterFiles.DisposalReasons.CreateMany
{
    public class CreateSingleDisposalReasonWithValidData : ScenarioFor<AsyncApiDriver, DisposalReasonStory>
    {
        private ApiResponse<RecordsCreatedResponse> _result;
        private List<DisposalReasonDto> _command;

        public void Given_a_valid_new_Master_File()
        {
            DisposalReasonDto dto = new DisposalReasonDtoBuilder()
                .Set(x => x.Id, 0);
            _command = dto.ToCollection();
        }

        public async Task When_I_attempt_to_create_it()
        {
            _result = await SUT.PostAsync<RecordsCreatedResponse>(
                ApiRoutes.Master.CreateFor<DisposalReason>(), _command);
        }

        public void Then_the_new_file_should_be_created()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.Created);
            _result.Model.NewIds[0].Should().BeGreaterThan(0);
            Db.Find<DisposalReason>(_result.Model.NewIds[0]).Should().NotBeNull();
        }

        public void AndThen_the_link_to_the_new_file_should_be_provided()
        {
            _result.Headers.Location.AbsolutePath
                .Should().Be(ApiRoutes.Master.GetFor<DisposalReason>(_result.Model.NewIds[0]));
        }
    }
}