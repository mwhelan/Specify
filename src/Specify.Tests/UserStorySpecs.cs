using FluentAssertions;
using NUnit.Framework;
using Specify.Tests.Stubs;

namespace Specify.Tests
{
    public class UserStorySpecs
    {
        [Test]
        public void should_have_default_title_prefix_if_none_is_provided()
        {
            var sut = new WithdrawCashUserStory();
            sut.TitlePrefix.Should().Be("Story: ");
        }

        [Test]
        public void should_provide_omitted_clause_prefixes()
        {
            var sut = new WithdrawCashUserStory();
            sut.IWant.Should().StartWith("I want");
        }

        [Test]
        public void should_allow_omitting_clauses()
        {
            var sut = new WithdrawCashUserStory();
            sut.SoThat.Should().BeNull();
        }

        [Test]
        public void should_be_able_to_set_title_and_title_prefix()
        {
            var sut = new TicTacToeUserStory();
            sut.Title.Should().Be("Tic Tac Toe Story");
            sut.TitlePrefix.Should().Be("User Story 1:");
        }
    }
}
