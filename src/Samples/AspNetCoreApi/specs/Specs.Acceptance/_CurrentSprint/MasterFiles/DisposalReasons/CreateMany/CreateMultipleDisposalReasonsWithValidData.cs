using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Contracts;
using ApiTemplate.Api.Contracts.Responses;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using FluentAssertions;
using Specs.Library.Builders.Entities.MasterFiles;
using Specs.Library.Drivers.Api;

namespace Specs.Acceptance._CurrentSprint.MasterFiles.DisposalReasons.CreateMany
{
    public class CreateMultipleDisposalReasonsWithValidData : ScenarioFor<AsyncApiDriver, DisposalReasonStory>
    {
        private ApiResponse<RecordsCreatedResponse> _result;
        private List<DisposalReasonDto> _command;

        public void Given_a_set_of_valid_new_Master_Files()
        {
            _command = DisposalReasonDtoBuilder.CreateListOfSize(3)
                .All()
                .Set(x => x.Id, 0);
        }

        public async Task When_I_attempt_to_create_them()
        {
            _result = await SUT.PostAsync<RecordsCreatedResponse>(
                ApiRoutes.Master.CreateFor<DisposalReason>(), _command);
        }

        public void Then_the_new_files_should_be_created()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.Created);
            _result.Model.NewIds[0].Should().BeGreaterThan(0);

            Db.Set<DisposalReason>().Should().HaveCount(3);
        }

        public void AndThen_the_link_to_the_first_new_file_should_be_provided()
        {
            _result.Headers.Location.AbsolutePath
                .Should().Be(ApiRoutes.Master.GetFor<DisposalReason>(_result.Model.NewIds[0]));
        }
    }
}