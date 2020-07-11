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
    public class CreateTodoItemCommand : CommandBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
    }

    public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
    {
        public CreateTodoItemCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(v => v.Description)
                .MaximumLength(200)
                .NotEmpty();
        }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, Result>
    {
        private readonly IUnitOfWork _context;

        public CreateTodoItemCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new ToDoItem(request.Title, request.Description, Email.Create(request.Email).Value);

            _context.ToDoItems.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Results.Ok()
                .WithReason(new RecordsCreatedSuccess(entity.Id));
        }
    }
}