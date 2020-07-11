namespace ApiTemplate.Api.Domain.Common.FluentResult
{
    // Base warning for all application warnings.
    public class AppWarning : Warning, IFailure
    {
        public string PropertyName { get; }
        public int RowKey { get; }

        public AppWarning(string propertyName, string message, int rowKey = int.MinValue)
            : this(message, rowKey)
        {
            PropertyName = propertyName;
        }

        public AppWarning(string message, int rowKey = int.MinValue)
            : base(message)
        {
            RowKey = rowKey;
            Message = message;
        }
    }
}