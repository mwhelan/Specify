using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Common.Validation;
using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApiTemplate.Api.Application.Common.Behaviours
{
    public class RequestValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : CommandBase
        where TResponse : Result
    {
        private readonly ValidationService _validationService;
        private readonly ILogger<TRequest> _logger;

        public RequestValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, ILogger<TRequest> logger)
        {
            _validationService = new ValidationService(validators);
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest command, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validationResult = _validationService.ValidateCommand(command);

            if (validationResult.IsFailed)
            {
                return validationResult as TResponse;
            }

            var handlerResult = await next();

            var result = _validationService.EvaluateResults(command.IgnoreWarnings, validationResult, handlerResult);

            return result as TResponse;
        }
    }
}