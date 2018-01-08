using NSubstitute;
using Shouldly;
using Specify.Samples.Domain.Atm;
using TestStack.BDDfy;

namespace Specify.Samples.Specs.ATM
{
    public class CardHasBeenDisabled : ScenarioFor<Atm, AccountHolderWithdrawsCashStory>
    {
        public void Given_the_Card_is_disabled()
        {
			The<Card>().Enabled.Returns(false);
			The<Card>().AccountBalance.Returns(100);
            SUT = new Atm(100);
        }

        [When("When the account holder requests $20")]
        public void When_the_Account_Holder_requests_20_dollars()
        {
            SUT.RequestMoney(The<Card>(), 20);
        }

        public void Then_the_Card_should_be_retained()
        {
            SUT.CardIsRetained.ShouldBe(true);
        }

        public void AndThen_the_ATM_should_say_the_Card_has_been_retained()
        {
            SUT.Message.ShouldBe(DisplayMessage.CardIsRetained);
        }
    }
}