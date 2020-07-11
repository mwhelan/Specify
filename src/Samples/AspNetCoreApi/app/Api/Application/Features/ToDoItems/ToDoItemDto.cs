using ApiTemplate.Api.Application.Common.Mappings;
using ApiTemplate.Api.Domain.Model.ToDos;
using AutoMapper;

namespace ApiTemplate.Api.Application.Features.ToDoItems
{
    public class ToDoItemDto : IMapFrom<ToDoItem>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public bool Done { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ToDoItem, ToDoItemDto>()
                .ForMember(d => d.Done,
                    opt => opt.MapFrom(s => s.IsDone));
        }
    }
}
