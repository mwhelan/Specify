using System;
using ApiTemplate.Api.Domain.Model.ToDos;
using FluentValidation.Resources;
using FluentValidation.Validators;

namespace ApiTemplate.Api.Application.Features.ToDoItems
{
    public class EmailValidator : PropertyValidator
    {
        public EmailValidator(IStringSource errorMessageSource) : base(errorMessageSource)
        {
        }

        public EmailValidator(string errorMessage) : base(errorMessage)
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var result = Email.Create(context.PropertyValue.ToString());
            return result.IsSuccess;
        }
    }
}