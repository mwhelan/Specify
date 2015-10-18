using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Tests.Stubs;

namespace Specify.Tests.Configuration
{
    [TestFixture]
    public class TestRunnerTests
    {
        [Test]
        public void should_call_per_test_actions_before_test()
        {
            var spec = new UserStoryScenarioWithAllSupportedStepsInRandomOrder();
            var sut = CreateSut(spec);

            sut.Execute(spec);

            StubPerScenarioAction.BeforeActions.Count.ShouldBe(2);
            StubPerScenarioAction.BeforeActions[0].ShouldBe("PerTestAction 1");
            StubPerScenarioAction.BeforeActions[1].ShouldBe("PerTestAction 2");
        }

        [Test]
        public void should_call_per_test_actions_after_test()
        {
            var spec = new UserStoryScenarioWithAllSupportedStepsInRandomOrder();
            var sut = CreateSut(spec);

            sut.Execute(spec);

            StubPerScenarioAction.AfterActions.Count.ShouldBe(2);
            StubPerScenarioAction.AfterActions[0].ShouldBe("PerTestAction 2");
            StubPerScenarioAction.AfterActions[1].ShouldBe("PerTestAction 1");
        }

        [Test]
        public void should_create_and_dispose_child_container_per_test()
        {
            var spec = new UserStoryScenarioWithAllSupportedStepsInRandomOrder();
            var sut = CreateSut(spec);

            sut.Execute(spec);

            sut.ApplicationContainer.Received(1).Resolve<IScenarioContainer>();
            sut.ChildContainer.Received(1).Dispose();
        }

        [Test]
        public void should_resolve_specification_from_child_container()
        {
            var spec = new UserStoryScenarioWithAllSupportedStepsInRandomOrder();
            var sut = CreateSut(spec);

            sut.Execute(spec);

            sut.ChildContainer.Received(1).Resolve(typeof(UserStoryScenarioWithAllSupportedStepsInRandomOrder));
        }

        private static TestableTestRunner CreateSut(IScenario spec)
        {
            return new TestableTestRunner(spec);
        }
    }
}
