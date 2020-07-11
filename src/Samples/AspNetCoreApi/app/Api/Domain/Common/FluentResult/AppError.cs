using FluentResults;

namespace ApiTemplate.Api.Domain.Common.FluentResult
{
    // Base error for all application errors.
    public class AppError : Error, IFailure
    {
        public string PropertyName { get; }
        public int RowKey { get; }

        public AppError(string propertyName, string message, int rowKey = int.MinValue)
            : this(message, rowKey)
        {
            PropertyName = propertyName;
        }

        public AppError(string message, int rowKey = int.MinValue)
        {
            RowKey = rowKey;
            Message = message;
        }
    }
}