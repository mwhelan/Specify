using System;
using NUnit.Framework;
using Shouldly;
using Specify.Configuration.Examples;
using Specify.Tests.Stubs;
using TestStack.BDDfy;

namespace Specify.IntegrationTests.Containers
{
    public abstract class ContainerSpecsFor<T> where T : IContainerRoot
    {
        protected abstract T CreateSut();

        [Test]
        public void Should_reset_SUT_for_every_example()
        {
            var container = CreateSut();
            var scenario = new ExampleScenario();
            scenario.SetTestScope(container.Get<TestScope>());
            Action action = scenario.Specify;
            action.ShouldNotThrow();
        }
    }

    class ExampleScenario : ScenarioFor<ConcreteObjectWithOneInterfaceConstructor>
    {
        private int _result;

        public ExampleScenario()
        {
            Examples = new ExampleTable("Num", "Result")
            {
                {1, 1},
                {2, 2}
            };
        }

        public void GivenTheValue(int num)
        {
            Container.Get<IDependency1>().Value = num;
        }

        public void WhenICheckTheDependencyValue()
        {
            _result = SUT.Dependency1.Value;
        }

        public void ThenItShouldBe_(int result)
        {
            _result.ShouldBe(result);
        }

        public override void RegisterContainerOverrides()
        {
            TestScope.Set<IDependency1,Dependency1>();
        }
    }
}