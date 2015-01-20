using FluentAssertions;
using NUnit.Framework;
using Specify.Tests.Stubs;

namespace Specify.Tests
{
    internal class UserStorySpecs : WithNunit.SpecificationFor<WithdrawCashUserStory>
    {
        public void When_creating_a_user_story()
        {
            
        }

        public void Then_should_have_default_title_prefix_if_none_is_provided()
        {
            SUT.TitlePrefix.Should().Be("Story: ");
        }

        public void AndThen_should_return_overridden_properties()
        {
            SUT.AsA.Should().Be("As an Account Holder");
        }

        public void AndThen_should_provide_omitted_clause_prefixes()
        {
            SUT.IWant.Should().StartWith("I want");
        }

        public void AndThen_should_allow_omitting_clauses()
        {
            SUT.SoThat.Should().BeNull();
        }

        //[Test]
        //public void should_be_able_to_set_title_and_title_prefix()
        //{
        //    var sut = new TicTacToeUserStory();
        //    sut.Title.Should().Be("Tic Tac Toe Story");
        //    sut.TitlePrefix.Should().Be("User Story 1:");
        //}
    }
}
