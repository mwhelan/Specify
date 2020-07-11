using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Common.Interfaces;
using ApiTemplate.Api.Domain.Common.FluentResult;
using FluentResults;
using MediatR;

namespace ApiTemplate.Api.Application.Features.ToDoItems
{
    public class DeleteTodoItemCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }

    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand, Result>
    {
        private readonly IUnitOfWork _context;

        public DeleteTodoItemCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ToDoItems.FindAsync(request.Id);

            if (entity == null)
            {
                return Resultz.RecordNotFound("Id", request.Id);
            }

            _context.ToDoItems.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Results.Ok();
        }
    }
}