using Specify.Samples.Domain.TrainFares;
using TestStack.BDDfy;
namespace Specify.Samples.Specs.TrainFares
{
    public class SuccessfulRailcardPurchasesWithExamples : ScenarioFor<object>
    {
#pragma warning disable 169
        // ReSharper disable once InconsistentNaming
        private Fare fare;
        private BuyerCategory _buyerCategory;
#pragma warning restore 169
        Money Price { get; set; }

        public SuccessfulRailcardPurchasesWithExamples()
        {
            Examples = (new ExampleTable(
                "Buyer Category", "Fare", "Price")
            {
                {BuyerCategory.Student, new MonthlyPass(), new Money(76)},
                {BuyerCategory.Senior, new MonthlyPass(), new Money(98)},
                {BuyerCategory.Standard, new MonthlyPass(), new Money(146)},
                {BuyerCategory.Student, new WeeklyPass(), new Money(23)},
                {BuyerCategory.Senior, new WeeklyPass(), new Money(30)},
                {BuyerCategory.Standard, new WeeklyPass(), new Money(44)},
                {BuyerCategory.Student, new DayPass(), new Money(4)},
                {BuyerCategory.Senior, new DayPass(), new Money(5)},
                {BuyerCategory.Standard, new DayPass(), new Money(7)},
                {BuyerCategory.Student, new SingleTicket(), new Money(1.5m)},
                {BuyerCategory.Senior, new SingleTicket(), new Money(2m)},
                {BuyerCategory.Standard, new SingleTicket(), new Money(3m)}
            });
        }
        void GivenTheBuyerIsA(BuyerCategory buyerCategory)
        {

        }

        void AndGivenTheBuyerSelectsA(Fare fare)
        {

        }

        void WhenTheBuyerPays()
        {

        }

        void ThenASaleOccursWithAnAmountOf(Money price)
        {

        }
    }
}