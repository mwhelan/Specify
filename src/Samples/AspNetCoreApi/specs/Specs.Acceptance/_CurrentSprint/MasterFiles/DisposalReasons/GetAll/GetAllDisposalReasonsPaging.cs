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
using TestStack.Dossier.Lists;

namespace Specs.Acceptance._CurrentSprint.MasterFiles.DisposalReasons.GetAll
{
    public class GetAllDisposalReasonsPaging : ScenarioFor<AsyncApiDriver, DisposalReasonStory>
    {
        private ApiResponse<PagedList<DisposalReasonDto>> _result;
        private IList<DisposalReason> _entityList;

        public void Given_7_DisposalReasons_where_5_contain_the_search_term()
        {
            _entityList = Builder<DisposalReason>.CreateListOfSize(7)
                .All().Set(x => x.Id, 0)
                .TheLast(5).Set(x => x.DisposalReasonDescription, "Avocado")
                .ListBuilder
                .Persist();
        }

        public void AndGiven_I_want_to_see_the_second_page_and_3_records_at_a_time() { }

        public async Task When_I_search_for_that_term()
        {
            _result = await SUT.GetAllPagedAsync<DisposalReasonDto>(
                ApiRoutes.Master.GetAllFor<DisposalReason>(pageSize: 3, page: 2, 
                    filterField: "DisposalReasonDescription", filterText: "Avo"));
        }

        public void Then_I_should_see_the_last_2_of_the_matching_5_records()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.OK);
            _result.Model.Results.Count.Should().Be(2);
            _result.Model.CurrentPage.Should().Be(2);
            _result.Model.PageSize.Should().Be(3);
            _result.Model.RowCount.Should().Be(5);

            _result.Model.Results[0].Id.Should().Be(_entityList[5].Id);
            _result.Model.Results[1].Id.Should().Be(_entityList[6].Id);
        }
    }
}