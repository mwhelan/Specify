using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Common.Paging;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Contracts;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using FluentAssertions;
using Specs.Library.Data;
using Specs.Library.Drivers.Api;
using TestStack.Dossier;

namespace Specs.Acceptance._CurrentSprint.MasterFiles.DisposalReasons.GetAll
{
    public class GetAllDisposalReasonsWhenSome : ScenarioFor<AsyncApiDriver, DisposalReasonStory>
    {
        private ApiResponse<PagedList<DisposalReasonDto>> _result;
        private IList<DisposalReason> _entityList;

        public void Given_a_collection_of_DisposalReasons()
        {
            _entityList = Builder<DisposalReason>.CreateListOfSize(3)
                .All()
                .Set(x => x.Id,0)
                .ListBuilder
                .Persist();
        }

        public async Task When_I_view_my_list()
        {
            _result = await SUT.GetAllPagedAsync<DisposalReasonDto>(ApiRoutes.Master.GetAllFor<DisposalReason>());
        }

        public void Then_I_should_see_the_whole_collection()
        {
            _result.Model.Results.Count.Should().Be(_entityList.Count);
            _result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}