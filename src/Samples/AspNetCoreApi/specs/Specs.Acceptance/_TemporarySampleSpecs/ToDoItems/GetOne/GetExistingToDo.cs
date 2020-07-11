using System.Net;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Features.ToDoItems;
using ApiTemplate.Api.Contracts;
using ApiTemplate.Api.Domain.Model.ToDos;
using FluentAssertions;
using Specs.Library.Builders.Entities;
using Specs.Library.Data;
using Specs.Library.Drivers.Api;

namespace Specs.Acceptance._TemporarySampleSpecs.ToDoItems.GetOne
{
    public class GetExistingToDo : ScenarioFor<AsyncApiDriver, ToDoStory>
    {
        private ApiResponse<ToDoItemDto> _result;
        private ToDoItem _existingItem;

        public void Given_I_have_created_a_to_do()
        {
            _existingItem = new ToDoItemBuilder().Persist();
        }

        public async Task When_I_attempt_to_view_it()
        {
             _result = await SUT.GetAsync<ToDoItemDto>(ApiRoutes.ToDo.GetFor(_existingItem.Id));
        }

        public void Then_I_should_see_all_the_things_I_have_to_do()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.OK);
            _result.Model.Id.Should().Be(_existingItem.Id);
        }
    }
}
