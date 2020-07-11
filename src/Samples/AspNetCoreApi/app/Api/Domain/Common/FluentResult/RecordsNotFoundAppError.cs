namespace ApiTemplate.Api.Domain.Common.FluentResult
{
    public class RecordsNotFoundAppError : AppError
    {
        public RecordsNotFoundAppError(string propertyName, int id)
            : base(propertyName, $"Record '{id}' not found", rowKey: id)
        {
        }

        public RecordsNotFoundAppError()
            : base("Records not found")
        {
        }
    }
}