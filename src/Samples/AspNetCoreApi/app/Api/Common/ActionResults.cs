using ApiTemplate.Api.Contracts.Responses;
using ApiTemplate.Api.Domain.Common.FluentResult;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Api.Common
{
    public static class ActionResults
    {
        public static IActionResult ValidationFailure(string propertyId, string validationMessage)
        {
            var validationError = Resultz.Error(propertyId, validationMessage);
            return new BadRequestObjectResult(new ErrorResponse(validationError));
        }
    }
}