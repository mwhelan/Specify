using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Containers;
using Specify.Tests.Stubs;

namespace Specify.Tests.Configuration
{
    [TestFixture]
    public class TestRunnerTests
    {
        [Test]
        public void should_call_per_test_actions_before_test()
        {
            var spec = new ScenarioWithAllSupportedStepsInRandomOrder();
            var sut = CreateSut(spec);

            sut.Execute(spec);

            StubAction.BeforeActions.Count.ShouldBe(2);
            StubAction.BeforeActions[0].ShouldBe("PerTestAction 1");
            StubAction.BeforeActions[1].ShouldBe("PerTestAction 2");
        }

        [Test]
        public void should_call_per_test_actions_after_test()
        {
            var spec = new ScenarioWithAllSupportedStepsInRandomOrder();
            var sut = CreateSut(spec);

            sut.Execute(spec);

            StubAction.AfterActions.Count.ShouldBe(2);
            StubAction.AfterActions[0].ShouldBe("PerTestAction 2");
            StubAction.AfterActions[1].ShouldBe("PerTestAction 1");
        }

        [Test]
        public void should_create_and_dispose_child_container_per_test()
        {
            var spec = new ScenarioWithAllSupportedStepsInRandomOrder();
            var sut = CreateSut(spec);

            sut.Execute(spec);

            sut.Container.Received(1).CreateChildContainer();
            sut.ChildContainer.Received(1).Dispose();
        }

        [Test]
        public void should_resolve_specification_from_container()
        {
            var spec = new ScenarioWithAllSupportedStepsInRandomOrder();
            var sut = CreateSut(spec);

            sut.Execute(spec);

            sut.Container.Received(1).Resolve(typeof(ScenarioWithAllSupportedStepsInRandomOrder));
        }

        private static TestableTestRunner CreateSut(ISpecification spec)
        {
            return new TestableTestRunner(spec);
        }
    }
}
