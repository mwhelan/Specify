using System.Collections.Generic;
using ApiTemplate.Api.Domain.Common;
using FluentResults;

namespace ApiTemplate.Api.Domain.Model.SharedValueObjects
{
    public class PositiveInteger : ValueObject
    {
        public int Value { get; }

        private PositiveInteger(int value)
        {
            Value = value;
        }

        public static Result<PositiveInteger> Create(int? value)
        {
            if (value == null)
                return Results.Fail<PositiveInteger>("Value cannot be empty.");

            if (value <= 0)
                return Results.Fail<PositiveInteger>("Value must greater than zero.");

            return Results.Ok(new PositiveInteger((int)value));
        }

        public static Result<PositiveInteger?> CreateNullable(int? value)
        {
            if (value == null)
                return Results.Ok((PositiveInteger?)null);

            return Create(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator int(PositiveInteger value)
        {
            return value.Value;
        }
    }
}
