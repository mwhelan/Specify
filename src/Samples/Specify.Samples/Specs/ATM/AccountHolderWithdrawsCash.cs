using Shouldly;
using Specify.Samples.Domain.Atm;
using TestStack.BDDfy;

namespace Specify.Samples.Specs.ATM
{
    public class AccountHasSufficientFunds : ScenarioFor<Atm, AccountHolderWithdrawsCashStory>
    {
        private Card _card;

        [Given("Given the account balance is $100")]
        public void Given_the_Account_Balance_is_100_dollars()
        {
            _card = new Card(true, 100);
        }

        public void AndGiven_the_Card_is_valid()
        {
        }

        public void AndGiven_the_machine_contains_enough_money()
        {
            SUT = new Atm(100);
        }

        [When("When the account holder requests $20")]
        public void When_the_Account_Holder_requests_20_dollars()
        {
            SUT.RequestMoney(_card, 20);
        }

        [Then("Then the ATM should dispense $20")]
        public void Then_the_ATM_should_dispense_20_dollars()
        {
            SUT.DispenseValue.ShouldBe(20);
        }

        [AndThen("And the account balance should be $80")]
        public void AndThen_the_Account_Balance_should_be_80_dollars()
        {
            _card.AccountBalance.ShouldBe(80);
        }

        public void AndThen_the_Card_should_not_be_retained()
        {
            SUT.CardIsRetained.ShouldBe(false);
        }
    }
}
