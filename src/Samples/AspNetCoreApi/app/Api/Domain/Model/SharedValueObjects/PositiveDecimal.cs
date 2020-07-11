using System.Collections.Generic;
using ApiTemplate.Api.Domain.Common;
using FluentResults;

namespace ApiTemplate.Api.Domain.Model.SharedValueObjects
{
    public class PositiveDecimal : ValueObject
    {
        public decimal Value { get; }

        private PositiveDecimal(decimal value)
        {
            Value = value;
        }

        public static Result<PositiveDecimal> Create(decimal value)
        {
            if (value <= 0)
                return Results.Fail<PositiveDecimal>("Value must greater than zero.");

            return Results.Ok(new PositiveDecimal(value));
        }

        public static Result<PositiveDecimal?> CreateNullable(decimal? value)
        {
            if (value == null)
                return Results.Ok((PositiveDecimal?)null);

            return Create((decimal)value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator decimal(PositiveDecimal value)
        {
            return value.Value;
        }
    }
}
