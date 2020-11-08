using System;
using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Common.Validation;
using ApiTemplate.Api.Domain.Common.FluentResult;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Specs.Library.Builders.ObjectMothers
{
    public class Stubs
    {
        public TestCommand TestCommand(bool ignoreWarnings = false)
        {
            return new TestCommand
            {
                Name = "Name",
                Reference = "Reference",
                StartDate = new DateTime(2019, 4, 1),
                EndDate = new DateTime(2019, 4, 2),
                IgnoreWarnings = ignoreWarnings
            };
        }

        public Result ResultWithErrorsAndWarnings()
        {
            var result = Resultz.Warning("warning message")
                .AddRecordsNotFound("Id", 22)
                .AddError("propertyName", "message");

            // these are base errors from FluentResults and are ignored
            result.Reasons.Add(new Error("error"));
            result.Reasons.Add(new Warning("message"));

            return (Result)result;
        }

    }

    public class TestCommand : CommandBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class TestCommandValidator : AbstractValidator<TestCommand>
    {
        public TestCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithSeverity(Severity.Error);
            RuleFor(x => x.Reference).NotEmpty().WithSeverity(Severity.Warning);
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate);
        }
    }

    public class TestCommandHandler : IRequestHandler<TestCommand, Result>
    {
        public Task<Result> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            // Do some stuff
            return Task.FromResult(Result.Ok());
        }
    }
}