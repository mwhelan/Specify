using ApiTemplate.Api.Domain.Model.SharedValueObjects;
using FluentAssertions;
using Specs.Library.Extensions;

namespace Specs.Unit.ApiTemplate.Domain.Model.SharedValueObjects
{
    public class ChemicalRateQuantitySpecs : ScenarioFor<ChemicalRateQuantity>
    {
        private const decimal PositiveDecimal = 9.3m;
        private const decimal NegativeDecimal = -1.2m;

        public void When_creating_ChemicalRateQuantity(){}

        public void Then_QuantityPer100Litre_or_QuantityPerHectare_should_be_positive_decimal()
        {
            var quantityPer100LitreResult = ChemicalRateQuantity.Create(PositiveDecimal, null);
            quantityPer100LitreResult.IsSuccess.Should().BeTrue();
            quantityPer100LitreResult.Value.QuantityPer100Litre.Should().Be(PositiveDecimal);

            var quantityPerHectareResult = ChemicalRateQuantity.Create(null, PositiveDecimal);
            quantityPerHectareResult.IsSuccess.Should().BeTrue();
            quantityPerHectareResult.Value.QuantityPerHectare.Should().Be(PositiveDecimal);
        }

        public void AndThen_should_not_be_created_if_both_QuantityPer100Litre_and_QuantityPerHectare_provided()
        {
            ChemicalRateQuantity.Create(PositiveDecimal, PositiveDecimal)
                .ShouldHaveError("Cannot enter both Quantity per 100L and Quantity per Hectare.");
        }

        public void AndThen_should_not_be_created_if_neither_QuantityPer100Litre_or_QuantityPerHectare_provided()
        {
            ChemicalRateQuantity.Create(null, null)
                .ShouldHaveError("Either Quantity per 100L or Quantity per Hectare is required.");
        }

        public void AndThen_should_not_be_created_if_either_QuantityPer100Litre_or_QuantityPerHectare_is_less_than_zero()
        {
            ChemicalRateQuantity.Create(NegativeDecimal, PositiveDecimal)
                .ShouldHaveError("Value must greater than zero.");
            ChemicalRateQuantity.Create(PositiveDecimal, NegativeDecimal)
                .ShouldHaveError("Value must greater than zero.");
        }

        public void AndThen_should_be_equal_if_same_QuantityPer100Litre_and_QuantityPerHectare()
        {
            ChemicalRateQuantity.Create(PositiveDecimal, null).Value
                .Should().Be(ChemicalRateQuantity.Create(PositiveDecimal, null).Value);
            
            ChemicalRateQuantity.Create(PositiveDecimal, null).Value
                .Should().NotBe(ChemicalRateQuantity.Create(null, PositiveDecimal).Value);
        }
    }
}