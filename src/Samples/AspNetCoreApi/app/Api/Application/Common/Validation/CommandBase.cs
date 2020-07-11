using FluentResults;
using MediatR;

namespace ApiTemplate.Api.Application.Common.Validation
{
    public class CommandBase : IRequest<Result>
    {
        public bool IgnoreWarnings { get; set; } = false;
    }
}