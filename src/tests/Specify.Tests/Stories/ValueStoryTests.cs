using NUnit.Framework;
using Shouldly;
using Specify.Tests.Stubs;

namespace Specify.Tests.Stories
{
    [TestFixture]
    public class ValueStoryTests
    {
        [Test]
        public void should_have_default_title_prefix_if_none_is_provided()
        {
            var sut = new WithdrawCashValueStory();
            sut.TitlePrefix.ShouldBe("Story: ");
        }

        [Test]
        public void should_return_overridden_properties()
        {
            var sut = new TicTacToeValueStory();
            sut.AsA.ShouldBe("As a player");
            sut.IWant.ShouldBe("I want to have a tic tac toe game");
        }

        [Test]
        public void should_provide_omitted_clause_prefixes()
        {
            var sut = new TicTacToeValueStory();
            sut.InOrderTo.ShouldBe("In order to waste some time!");
        }

        [Test]
        public void should_allow_omitting_clauses()
        {
            var sut = new WithdrawCashValueStory();
            sut.InOrderTo.ShouldBe(null);
        }

        [Test]
        public void should_be_able_to_set_title_and_title_prefix()
        {
            var sut = new TicTacToeValueStory();
            sut.Title.ShouldBe("Tic Tac Toe Story");
            sut.TitlePrefix.ShouldBe("User Story 1:");
        }
    }
}