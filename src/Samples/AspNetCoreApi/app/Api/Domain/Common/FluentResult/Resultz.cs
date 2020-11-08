using FluentResults;

namespace ApiTemplate.Api.Domain.Common.FluentResult
{
    public static class Resultz
    {
        public static Result RecordNotFound(string propertyName, int id)
        {
            return Result.Ok()
                .WithReason(new RecordsNotFoundAppError(propertyName, id));
        }

        public static Result Error(string propertyName, string message, int rowKey = int.MinValue)
        {
            return Result.Ok()
                .WithReason(new AppError(propertyName, message, rowKey));
        }

        public static Result Error(string message, int rowKey = int.MinValue)
        {
            return Result.Ok()
                .WithReason(new AppError(message, rowKey));
        }

        public static Result Warning(string propertyName, string message, int rowKey = int.MinValue)
        {
            return Result.Ok()
                .WithReason(new AppWarning(propertyName, message, rowKey));
        }

        public static Result Warning(string message, int rowKey = int.MinValue)
        {
            return Result.Ok()
                .WithReason(new AppWarning(message, rowKey));
        }

        public static Result GetResultForFailure(ValidationSeverity validationSeverity,
            string parameterName, string message, int rowKey = int.MinValue)
        {
            return validationSeverity switch
            {
                ValidationSeverity.Error => Resultz.Error(parameterName, message, rowKey),
                ValidationSeverity.Warning => Resultz.Warning(parameterName, message, rowKey),
                _ => throw new System.NotImplementedException()
            };
        }

        public static Reason GetReasonForFailure(ValidationSeverity validationSeverity,
            string parameterName, string message, int rowKey = int.MinValue)
        {
            return validationSeverity switch
            {
                ValidationSeverity.Error => new AppError(parameterName, message, rowKey),
                ValidationSeverity.Warning => new AppWarning(parameterName, message, rowKey),
                _ => throw new System.NotImplementedException()
            };
        }
    }
}