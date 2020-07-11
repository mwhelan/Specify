using FluentValidation;
using FluentValidation.Results;

namespace ApiTemplate.Api.Application.Common.Validation
{
    public abstract class PrimaryKeyValidator<T> : AbstractValidator<T> 
        where T : IRequireValidation
    {
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var result = base.Validate(context);
            InjectPrimaryKeyToValidationFailure(result, context);
            return result;
        }

        //public override async Task<ValidationResult> ValidateAsync(ValidationContext<T> context, CancellationToken cancellation = new CancellationToken())
        //{
        //    var result = await base.ValidateAsync(context, cancellation);
        //    InjectPrimaryKeyToValidationFailure(result, context);
        //    return result;
        //}

        private void InjectPrimaryKeyToValidationFailure(ValidationResult result, ValidationContext<T> context)
        {
            foreach (var failure in result.Errors)
            {
                if (failure.CustomState == null)
                {
                    failure.CustomState = context.InstanceToValidate.Id;
                }
            }
        }
    }
}