using System.Collections.Generic;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Common.Paging;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Contracts;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using FluentAssertions;
using Specs.Library.Data;
using Specs.Library.Drivers.Api;
using TestStack.BDDfy;
using TestStack.Dossier;
using TestStack.Dossier.DataSources.Picking;

namespace Specs.Acceptance._CurrentSprint.MasterFiles.DisposalReasons.GetAll
{
    public class GetAllDisposalReasonsOrdering : ScenarioFor<AsyncApiDriver, DisposalReasonStory>
    {
        private ApiResponse<PagedList<DisposalReasonDto>> _result;
        private IList<DisposalReason> _entityList;

        public GetAllDisposalReasonsOrdering()
        {
            Examples = new ExampleTable("Descending Order") {true, false};
        }
        
        public void Given_a_list_of_DisposalReasons()
        {
            var codes = new List<string> { "a", "b", "c", "d", "e", "f", "g" };
            _entityList = Builder<DisposalReason>.CreateListOfSize(7)
                .All()
                .Set(x => x.Id, 0)
                .Set(x => x.Reason, Pick.RepeatingSequenceFrom(codes).Next)
                .ListBuilder
                .Persist();
        }

        public async Task When_I_specify_sort_order(bool descendingOrder)
        {
            _result = await SUT.GetAllPagedAsync<DisposalReasonDto>(
                ApiRoutes.Master.GetAllFor<DisposalReason>(orderBy: "Reason", orderByDesc: descendingOrder));
        }

        public void Then_the_list_is_presented_in_the_specified_order(bool descendingOrder)
        {
            if (descendingOrder)
            {
                _result.Model.Results.Should().BeInDescendingOrder(x => x.Reason);
            }
            else
            {
                _result.Model.Results.Should().BeInAscendingOrder(x => x.Reason);
            }
        }
    }
}