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

            StubAction.BeforeActions.Count.ShouldBe(2);
            StubAction.BeforeActions[0].ShouldBe("PerTestAction 1");
            StubAction.BeforeActions[1].ShouldBe("PerTestAction 2");
        }

        [Test]
        public void should_call_per_test_actions_after_test()
        {
            var spec = new UserStoryScenarioWithAllSupportedStepsInRandomOrder();
            var sut = CreateSut(spec);

            sut.Execute(spec);

            StubAction.AfterActions.Count.ShouldBe(2);
            StubAction.AfterActions[0].ShouldBe("PerTestAction 2");
            StubAction.AfterActions[1].ShouldBe("PerTestAction 1");
        }

        [Test]
        public void should_create_and_dispose_child_container_per_test()
        {
            var spec = new UserStoryScenarioWithAllSupportedStepsInRandomOrder();
            var sut = CreateSut(spec);

            sut.Execute(spec);

            sut.DependencyResolver.Received(1).CreateChildContainer();
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
