using System.Net;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Features.ToDoItems;
using ApiTemplate.Api.Contracts;
using FluentAssertions;
using Specs.Library.Drivers.Api;

namespace Specs.Acceptance._TemporarySampleSpecs.ToDoItems.GetOne
{
    public class GetNonExistingToDo : ScenarioFor<AsyncApiDriver, ToDoStory>
    {
        private ApiResponse<ToDoItemDto> _result;

        public async Task When_I_attempt_to_view_a_todo_that_does_not_exist()
        {
            _result = await SUT.GetAsync<ToDoItemDto>(ApiRoutes.ToDo.GetFor(99));
        }

        public void Then_I_should_receive_a_not_found_warning()
        {
            //_result.Model.Should().BeNull();
            _result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}