using System;
using NUnit.Framework;
using Shouldly;
using Specify.Configuration;
using Specify.Exceptions;
using Specify.Tests.Configuration;
using Specify.Tests.Stubs;
using TestStack.BDDfy;

namespace Specify.IntegrationTests.Containers
{
    public abstract class ContainerSpecsFor<T> where T : IContainer
    {
        protected abstract T CreateSut();

        [Test]
        public void Should_reset_SUT_for_every_example()
        {
            var container = CreateSut();
            var scenario = new ExampleScenario();
            scenario.SetContainer(container);
            var engine = new BddfyTestEngine();
            Action action = () => engine.Execute(scenario);
            action.ShouldNotThrow();
        }
    }

    class ExampleScenario : ScenarioFor<ConcreteObjectWithOneInterfaceConstructor>
    {
        private int _result;
        private IDependency1 _dependency;

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
            _dependency = new Dependency1 { Value = num };
            Container.Set<IDependency1>(_dependency);
        }

        public void WhenICheckTheDependencyValue()
        {
            _result = SUT.Dependency1.Value;
        }

        public void ThenItShouldBe_(int result)
        {
            _result.ShouldBe(result);
        }
    }
}