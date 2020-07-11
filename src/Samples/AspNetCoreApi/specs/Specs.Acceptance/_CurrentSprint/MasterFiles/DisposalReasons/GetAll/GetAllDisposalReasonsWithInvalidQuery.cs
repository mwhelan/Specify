using System.Threading.Tasks;
using ApiTemplate.Api.Application.Common.Paging;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Contracts;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using FluentAssertions;
using Specs.Library.Drivers.Api;
using Specs.Library.Extensions;

namespace Specs.Acceptance._CurrentSprint.MasterFiles.DisposalReasons.GetAll
{
    public class GetAllDisposalReasonsWithInvalidQuery : ScenarioFor<AsyncApiDriver, DisposalReasonStory>
    {
        private ApiResponse<PagedList<DisposalReasonDto>> _result;

        public async Task When_I_search_with_invalid_search_parameters()
        {
            // FilterField without FilterText is invalid
            _result = await SUT.GetAllPagedAsync<DisposalReasonDto>(
                ApiRoutes.Master.GetAllFor<DisposalReason>(pageSize: 3, page: 2, filterField: "DisposalReasonDescription"));
        }

        public void Then_I_am_advised_of_the_error()
        {
            _result.ShouldHaveError("FilterText", "'FilterText' is required.");
        }

        public void AndThen_no_results_are_returned()
        {
            _result.Model.Should().BeNull();
        }
    }
}