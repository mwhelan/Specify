using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Common.Interfaces;
using ApiTemplate.Api.Domain.Model.ToDos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Api.Application.Features.ToDoItems
{
    public class GetAllToDosQuery : IRequest<IEnumerable<ToDoItemDto>>
    {
    }

    public class GetAllToDosQueryHandler : IRequestHandler<GetAllToDosQuery, IEnumerable<ToDoItemDto>>
    {
        private readonly IQueryDb _db;
        private readonly IMapper _mapper;

        public GetAllToDosQueryHandler(IQueryDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ToDoItemDto>> Handle(GetAllToDosQuery request, CancellationToken cancellationToken)
        {
            var items = await _db.QueryFor<ToDoItem>()
                .ProjectTo<ToDoItemDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Title)
                .ToListAsync(cancellationToken);

            return items;
        }
    }
}
