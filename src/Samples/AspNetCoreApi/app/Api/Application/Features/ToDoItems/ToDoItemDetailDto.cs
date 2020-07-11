using ApiTemplate.Api.Application.Common.Mappings;
using ApiTemplate.Api.Domain.Model.ToDos;

namespace ApiTemplate.Api.Application.Features.ToDoItems
{
    public class ToDoItemDetailDto : IMapFrom<ToDoItem>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public bool IsDone { get; set; }
    }
}