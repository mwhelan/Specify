using ApiTemplate.Api.Application.Features.ToDoItems;
using Swashbuckle.AspNetCore.Filters;

namespace ApiTemplate.Api.SwaggerExamples.Requests
{
    public class CreateTodoItemCommandExample : IExamplesProvider<CreateTodoItemCommand>
    {
        public CreateTodoItemCommand GetExamples()
        {
            return new CreateTodoItemCommand
            {
                Title = "Run and Review Tests",
                Description = "Make sure all the tests run and review what they are doing.",
                Email = "tests are important"
            };
        }
    }
}