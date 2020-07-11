using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Features.ToDoItems;
using ApiTemplate.Api.Contracts;
using FluentAssertions;
using Specs.Library.Builders.Entities;
using Specs.Library.Data;
using Specs.Library.Drivers.Api;

namespace Specs.Acceptance._TemporarySampleSpecs.ToDoItems.GetAll
{
    public class GetAllToDos : ScenarioFor<AsyncApiDriver, ToDoStory>
    {
        private ApiResponse<List<ToDoItemDto>> _result;

        public void Given_I_have_created_a_list_of_things_to_do()
        {
            ToDoItemBuilder.CreateDefaultList().Persist();
        }

        public async Task When_I_view_my_list()
        {
             _result = await SUT.GetAllAsync<ToDoItemDto>(ApiRoutes.ToDo.GetAll);
        }

        public void Then_I_should_see_all_the_things_I_have_to_do()
        {
            _result.Model.Count.Should().Be(3);
            _result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
