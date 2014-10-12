using FluentAssertions;
using NUnit.Framework;
using Specify.Tests.Stubs;

namespace Specify.Tests
{
    public class ValueStorySpecs
    {
        [Test]
        public void should_have_default_title_prefix_if_none_is_provided()
        {
            var sut = new WithdrawCashValueStory();
            sut.TitlePrefix.Should().Be("Story: ");
        }

        [Test]
        public void should_return_overridden_properties()
        {
            var sut = new TicTacToeValueStory();
            sut.AsA.Should().Be("As a player");
            sut.IWant.Should().Be("I want to have a tic tac toe game");
        }
        
        [Test]
        public void should_provide_omitted_clause_prefixes()
        {
            var sut = new TicTacToeValueStory();
            sut.InOrderTo.Should().Be("In order to waste some time!");
        }

        [Test]
        public void should_allow_omitting_clauses()
        {
            var sut = new WithdrawCashValueStory();
            sut.InOrderTo.Should().BeNull();
        }

        [Test]
        public void should_be_able_to_set_title_and_title_prefix()
        {
            var sut = new TicTacToeValueStory();
            sut.Title.Should().Be("Tic Tac Toe Story");
            sut.TitlePrefix.Should().Be("User Story 1:");
        }
    }
}