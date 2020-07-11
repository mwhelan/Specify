namespace ApiTemplate.Api.Domain.Common.FluentResult
{
    public class ValidationMessages
    {
        public const string MustBeValidPostCode = " must be a valid post code.";
        public const string DoesNotExist = " does not exist.";
        public const string IsRequired = " is required.";
        public const string CannotBeInPast = " cannot be in the past.";
        public const string IsInvalid = " is invalid.";
        public const string MustBeGreaterThanZero = " must be > 0.";
        public const string MustBePositive = " must be a positive number.";
        public const string AlreadyAssociatedWithAnotherParent = " is already asociated with another parent.";

        public static string IsRequiredFor(string propertyName)
        {
            return $"'{propertyName}'{IsRequired}";
        }
    }
}
