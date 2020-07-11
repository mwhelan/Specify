using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Common.Interfaces;
using ApiTemplate.Api.Application.Common.Validation;
using ApiTemplate.Api.Domain.Common.FluentResult;
using ApiTemplate.Api.Domain.Model.ToDos;
using FluentResults;
using FluentValidation;
using MediatR;

namespace ApiTemplate.Api.Application.Features.ToDoItems
{
    public class UpdateTodoItemCommand : CommandBase
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public bool IsDone { get; set; }
    }

    public class UpdateTodoItemCommandValidator : AbstractValidator<UpdateTodoItemCommand>
    {
        public UpdateTodoItemCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(v => v.Description)
                .MaximumLength(200)
                .NotEmpty();
        }
    }

    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, Result>
    {
        private readonly IUnitOfWork _context;

        public UpdateTodoItemCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ToDoItems.FindAsync(request.Id);

            if (entity == null)
            {
                return Resultz.RecordNotFound("Id", request.Id);
            }

            entity.Update(request.Title, request.Description, Email.Create(request.Email).Value, request.IsDone);

            await _context.SaveChangesAsync(cancellationToken);

            return Results.Ok();
        }
    }
}
