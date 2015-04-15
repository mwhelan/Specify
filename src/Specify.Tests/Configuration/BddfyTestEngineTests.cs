using ApprovalTests;
using ApprovalTests.Reporters;
using NSubstitute;
using NUnit.Framework;
using Specify.Configuration;
using Specify.Tests.Stubs;
using TestStack.BDDfy;
using TestStack.BDDfy.Reporters;

namespace Specify.Tests.Configuration
{
    using System.Runtime.CompilerServices;

    [TestFixture]
    public class BddfyTestEngineTests
    {
        [Test]
        public void should_handle_specification_with_examples()
        {
            var examples = new ExampleTable();
            var spec = Substitute.For<IScenario>();
            spec.Examples.Returns(examples);
            var sut = new BddfyTestEngine();

            sut.Execute(spec);

            spec.Received().WithExamples(examples);
        }

        [Test]
        public void should_handle_specification_without_examples()
        {
            var spec = Substitute.For<IScenario>();
            var sut = new BddfyTestEngine();

            sut.Execute(spec);

            spec.Received().BDDfy();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void should_display_examples_on_reports()
        {
            var scenario = new StubUserStoryScenarioForWithExamples();
            var sut = new BddfyTestEngine();

            var story = sut.Execute(scenario);

            var reporter = new TextReporter();
            reporter.Process(story);
            Approvals.Verify(reporter.ToString());
        }
    }
}
