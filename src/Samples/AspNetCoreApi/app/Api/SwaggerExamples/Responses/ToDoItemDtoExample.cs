using ApiTemplate.Api.Application.Features.ToDoItems;
using Swashbuckle.AspNetCore.Filters;

namespace ApiTemplate.Api.SwaggerExamples.Responses
{
    public class ToDoItemDtoExample : IExamplesProvider<ToDoItemDto>
    {
        public ToDoItemDto GetExamples()
        {
            return new ToDoItemDto
            {
                Id = 39,
                Title = "Run and Review Tests",
                Description = "Make sure all the tests run and review what they are doing.",
                Email = "tests are important",
                Done = true
            };
        }
    }
}