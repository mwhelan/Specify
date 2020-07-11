using System.Net;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Common.Paging;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Contracts;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using FluentAssertions;
using Specs.Library.Drivers.Api;

namespace Specs.Acceptance._CurrentSprint.MasterFiles.DisposalReasons.GetAll
{
    public class GetAllDisposalReasonsWhenNone : ScenarioFor<AsyncApiDriver, DisposalReasonStory>
    {
        private ApiResponse<PagedList<DisposalReasonDto>> _result;

        public void Given_there_are_no_DisposalReasons()
        {
        }

        public async Task When_I_view_the_list()
        {
            _result = await SUT.GetAllPagedAsync<DisposalReasonDto>(ApiRoutes.Master.GetAllFor<DisposalReason>());
        }

        public void Then_I_should_see_an_empty_list()
        {
            _result.Model.Results.Count.Should().Be(0);
            _result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}