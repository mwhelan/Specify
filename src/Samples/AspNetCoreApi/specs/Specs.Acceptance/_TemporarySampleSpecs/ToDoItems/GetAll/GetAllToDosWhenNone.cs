using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Features.ToDoItems;
using ApiTemplate.Api.Contracts;
using FluentAssertions;
using Specs.Library.Drivers.Api;

namespace Specs.Acceptance._TemporarySampleSpecs.ToDoItems.GetAll
{
    public class GetAllToDosWhenNone : ScenarioFor<AsyncApiDriver, ToDoStory>
    {
        private ApiResponse<List<ToDoItemDto>> _result;

        public void Given_I_do_not_have_any_things_to_do()
        {
        }

        public async Task When_I_view_my_to_do_list()
        {
            _result = await SUT.GetAllAsync<ToDoItemDto>(ApiRoutes.ToDo.GetAll);
        }

        public void Then_I_should_see_an_empty_list()
        {
            _result.Model.Count.Should().Be(0);
            _result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}