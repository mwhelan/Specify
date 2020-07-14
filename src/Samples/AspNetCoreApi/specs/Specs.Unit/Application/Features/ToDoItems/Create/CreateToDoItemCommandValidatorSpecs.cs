using ApiTemplate.Api.Application.Features.ToDoItems;
using FluentValidation.TestHelper;
using Specs.Library.Extensions;

namespace Specs.Unit.ApiTemplate.Application.Features.ToDoItems.Create
{
    public class CreateToDoItemCommandValidatorSpecs: ScenarioFor<CreateTodoItemCommandValidator>
    {
        public void Then_should_have_Title_with_no_more_than_200_characters()
        {
            SUT.ShouldHaveValidationErrorFor(x => x.Title, "a".Repeat(201))
                .WithErrorMessage("The length of 'Title' must be 200 characters or fewer. You entered 201 characters.");
            SUT.ShouldNotHaveValidationErrorFor(x => x.Title, "a".Repeat(100));
        }
    }
}