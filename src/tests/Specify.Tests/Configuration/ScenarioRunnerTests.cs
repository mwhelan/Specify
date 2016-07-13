using System;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Configuration;
using Specify.Tests.Stubs;

namespace Specify.Tests.Configuration
{
    internal class ScenarioRunnerTests : TestsFor<ScenarioRunner>
    {
        private UserStoryScenarioWithAllSupportedStepsInRandomOrder _spec;
        private IContainer _childContainer;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            Container.Set<IBootstrapSpecify>(new StubConfig());

            _spec = new UserStoryScenarioWithAllSupportedStepsInRandomOrder();
            _childContainer = Substitute.For<IContainer>();
            _childContainer.Get(Arg.Any<Type>()).Returns(_spec);

            SUT.Configuration.ApplicationContainer.Get<IContainer>().Returns(_childContainer);
        }

        [Test]
        public void should_call_per_scenario_actions_before_scenario_starts()
        {
            SUT.Execute(_spec);

            StubPerScenarioAction.BeforeActions.Count.ShouldBe(2);
            StubPerScenarioAction.BeforeActions[0].ShouldBe("PerTestAction 1");
            StubPerScenarioAction.BeforeActions[1].ShouldBe("PerTestAction 2");
        }

        [Test]
        public void should_call_per_scenario_actions_after_scenario_completes()
        {
            SUT.Execute(_spec);

            StubPerScenarioAction.AfterActions.Count.ShouldBe(2);
            StubPerScenarioAction.AfterActions[0].ShouldBe("PerTestAction 2");
            StubPerScenarioAction.AfterActions[1].ShouldBe("PerTestAction 1");
        }

        [Test]
        public void should_create_and_dispose_child_container_per_scenario()
        {
            SUT.Execute(_spec);

            SUT.Configuration.ApplicationContainer.Received(1).Get<IContainer>();
            _childContainer.Received(1).Dispose();
        }

        [Test]
        public void should_resolve_specification_from_child_container()
        {
            SUT.Execute(_spec);

            _childContainer.Received(1).Get(typeof(UserStoryScenarioWithAllSupportedStepsInRandomOrder));
        }
    }
}
