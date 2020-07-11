using System;
using System.Linq;
using ApiTemplate.Api.Contracts.Responses;
using ApiTemplate.Api.Domain.Common.FluentResult;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Api.Common
{
    public static class AspNetResultExtensions
    {
        public static IActionResult ToCreatedResult(this Result result, 
            string controllerName, string actionName = "Get")
        {
            if (result.IsSuccess)
            {
                var reason = result.GetReason<RecordsCreatedSuccess>();
                var response = new SuccessResponse<RecordsCreatedResponse>(new RecordsCreatedResponse(reason));

                return new CreatedAtActionResult(actionName, controllerName, 
                    new { id = reason.NewIds.First() }, response);
            }
            else
            {
                return result.ToFailureResult();
            }
        }

        public static IActionResult ToUpdatedResult(this Result result)
        {
            return result.IsSuccess ? new NoContentResult() : result.ToFailureResult();
        }

        public static IActionResult ToDeletedResult(this Result result)
        {
            return result.IsSuccess ? new NoContentResult() : result.ToFailureResult();
        }

        public static IActionResult ToFailureResult(this Result result)
        {
            if (result.IsSuccess)
            {
                throw new ArgumentException("Must be failed result");
            }

            var reason = result.GetReasonOrDefault<AppError>();
            if (reason is RecordsNotFoundAppError)
            {
                return new NotFoundResult();
            }

            return new BadRequestObjectResult(new ErrorResponse(result));
        }
    }
}