using Shouldly;
using Specify.Samples.Domain.Atm;
using TestStack.BDDfy;

namespace Specify.Samples.Specs.ATM
{
    public class AccountHasInsufficientFunds : ScenarioFor<Atm, AccountHolderWithdrawsCashStory>
    {
        private Card _card;

        // You can override step text using executable attributes
        [Given("Given the Account Balance is $10")]
        void GivenTheAccountBalanceIs10()
        {
            _card = new Card(true, 10);
        }

        void And_given_the_Card_is_valid()
        {
        }

        void AndGivenTheMachineContainsEnoughMoney()
        {
            SUT = new Atm(100);
        }

        [When("When the Account Holder requests $20")]
        void WhenTheAccountHolderRequests20()
        {
            SUT.RequestMoney(_card, 20);
        }

        void Then_the_ATM_should_not_dispense_any_Money()
        {
            SUT.DispenseValue.ShouldBe(0);
        }

        void And_the_ATM_should_say_there_are_Insufficient_Funds()
        {
            SUT.Message.ShouldBe(DisplayMessage.InsufficientFunds);
        }

        [AndThen("And the Account Balance should be $20")]
        void AndTheAccountBalanceShouldBe20()
        {
            _card.AccountBalance.ShouldBe(10);
        }

        void And_the_Card_should_be_returned()
        {
            SUT.CardIsRetained.ShouldBe(false);
        }
    }
}
