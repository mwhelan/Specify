using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Configuration;
using Specify.Tests.Stubs;
using TestStack.BDDfy;

namespace Specify.Tests.Configuration
{
    [TestFixture]
    public class BddfyTestEngineTests
    {
        [Test]
        public void should_handle_specification_with_examples()
        {
            var spec = new StubUserStoryScenarioForWithExamples();
            var sut = new BddfyTestEngine();

            sut.Execute(spec);

            spec.ExamplesWasCalled.ShouldBe(2);
        }

        [Test]
        public void should_handle_specification_without_examples()
        {
            var spec = Substitute.For<IScenario>();
            var sut = new BddfyTestEngine();

            sut.Execute(spec);

            spec.Received().BDDfy();
        }
    }
}