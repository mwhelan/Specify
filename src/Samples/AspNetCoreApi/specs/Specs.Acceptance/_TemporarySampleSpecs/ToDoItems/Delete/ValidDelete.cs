using System.Threading.Tasks;
using ApiTemplate.Api.Contracts;
using ApiTemplate.Api.Domain.Model.ToDos;
using FluentAssertions;
using Specs.Library.Builders.Entities;
using Specs.Library.Data;
using Specs.Library.Drivers.Api;

namespace Specs.Acceptance._TemporarySampleSpecs.ToDoItems.Delete
{
    public class ValidDelete : ScenarioFor<AsyncApiDriver, ToDoStory>
    {
        private ApiResponse _result;
        private ToDoItem _existingItem;

        public void Given_I_have_an_existing_todo()
        {
            _existingItem = new ToDoItemBuilder().Persist();
        }

        public async Task When_I_attempt_to_delete_it()
        {
            _result = await SUT.DeleteWithCheckAsync(ApiRoutes.ToDo.DeleteFor(_existingItem.Id));
        }

        public void Then_the_todo_should_be_deleted()
        {
            Db.Set<ToDoItem>().Should().HaveCount(0);
           // _result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }
    }
}