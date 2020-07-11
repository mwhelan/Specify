using System.Collections.Generic;
using ApiTemplate.Api.Domain.Common.FluentResult;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;

namespace ApiTemplate.Api.Application.Common.Validation
{
    public static class ValidationExtensions
    {
        public static Result WithValidationResults(this Result result, List<ValidationFailure> failures, int idForRow = int.MinValue)
        {
            foreach (var failure in failures)
            {
                var reason = failure.MapToReason(idForRow); 
                result.Reasons.Add(reason);
            }

            return result;
        }

        public static Reason MapToReason(this ValidationFailure failure, int idForRow)
        {
            var rowKey = idForRow;
            if (idForRow == int.MinValue)
            {
                rowKey = failure.CustomState == null ? int.MinValue : int.Parse(failure.CustomState.ToString());
            }

            return failure.Severity switch
            {
                Severity.Error => new AppError(failure.PropertyName, failure.ErrorMessage, rowKey),
                Severity.Warning => new AppWarning(failure.PropertyName, failure.ErrorMessage, rowKey),
                _ => throw new System.NotImplementedException()
            };
        }
    }
}