namespace ApiTemplate.Api.Domain.Common.FluentResult
{
    public interface IFailure
    {
        string PropertyName { get; }
        int RowKey { get; }
        string Message { get; }
    }
}