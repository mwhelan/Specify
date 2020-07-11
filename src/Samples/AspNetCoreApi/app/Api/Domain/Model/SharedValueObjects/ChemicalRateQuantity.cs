using System.Collections.Generic;
using ApiTemplate.Api.Domain.Common;
using ApiTemplate.Api.Domain.Common.FluentResult;
using FluentResults;

namespace ApiTemplate.Api.Domain.Model.SharedValueObjects
{
    public class ChemicalRateQuantity : ValueObject
    {
        public decimal? QuantityPer100Litre { get; }
        public decimal? QuantityPerHectare { get; }

        protected ChemicalRateQuantity() { }

        private ChemicalRateQuantity(PositiveDecimal? quantityPer100Litre, PositiveDecimal? quantityPerHectare)
        {
            QuantityPer100Litre = quantityPer100Litre?.Value;
            QuantityPerHectare = quantityPerHectare?.Value;
        }

        public static Result<ChemicalRateQuantity> Create(decimal? quantityPer100Litre, decimal? quantityPerHectare)
        {
            Result result = Results.Ok()
                .AddResult(PositiveDecimal.CreateNullable(quantityPer100Litre), out Result<PositiveDecimal?> qtyPer100L)
                .AddResult(PositiveDecimal.CreateNullable(quantityPerHectare), out Result<PositiveDecimal?> qtyPerHa);

            if (result.IsFailed)
            {
                return result;
            }

            if (qtyPer100L.Value == null && qtyPerHa.Value == null)
                result.AddError("Either Quantity per 100L or Quantity per Hectare is required.");

            if (qtyPer100L.Value != null && qtyPerHa.Value != null)
                result.AddError("Cannot enter both Quantity per 100L and Quantity per Hectare.");

            if (result.IsFailed)
                return result;

            return Results.Ok(new ChemicalRateQuantity(qtyPer100L.Value, qtyPerHa.Value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return QuantityPer100Litre;
            yield return QuantityPerHectare;
        }
    }
}
