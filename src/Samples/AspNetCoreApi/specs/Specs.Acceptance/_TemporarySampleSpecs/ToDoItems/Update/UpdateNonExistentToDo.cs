using System.Net;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Features.ToDoItems;
using ApiTemplate.Api.Contracts;
using FluentAssertions;
using Specs.Library.Builders;
using Specs.Library.Drivers.Api;

namespace Specs.Acceptance._TemporarySampleSpecs.ToDoItems.Update
{
    public class UpdateNonExistentToDo : ScenarioFor<AsyncApiDriver, ToDoStory>
    {
        private ApiResponse _result;
        private UpdateTodoItemCommand _updates;

        public void Given_I_am_trying_to_edit_a_ToDo_that_does_not_exist()
        {
            _updates = Get.InstanceOf<UpdateTodoItemCommand>();
        }

        public async Task When_I_attempt_to_apply_changes_to_it()
        {
            _result = await SUT.PutAsync(ApiRoutes.ToDo.Update, _updates);
        }

        public void Then_I_should_be_warned_that_the_ToDo_does_not_exist()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}