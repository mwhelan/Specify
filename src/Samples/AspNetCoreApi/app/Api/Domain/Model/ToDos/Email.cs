using System.Collections.Generic;
using System.Text.RegularExpressions;
using ApiTemplate.Api.Domain.Common;
using FluentResults;

namespace ApiTemplate.Api.Domain.Model.ToDos
{
    public class Email : ValueObject
    {
        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        public static Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Results.Fail<Email>("Email should not be empty");

            email = email.Trim();

            if (email.Length > 200)
                return Results.Fail<Email>("Email is too long");

            if (!Regex.IsMatch(email, @"^(.+)@(.+)$"))
                return Results.Fail<Email>("Email is invalid");

            return Results.Ok(new Email(email));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(Email email)
        {
            return email.Value;
        }
    }
}