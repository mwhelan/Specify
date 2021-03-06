﻿using System;
using System.Text.RegularExpressions;
using Autofac;
using DryIoc;
using NUnit.Framework;
using Shouldly;
using Specify.Autofac;
using Specify.Configuration;
using Specify.Configuration.StepScanners;
using Specify.Containers;
using Specify.Mocks;
using Specify.Tests.Stubs;
using TestStack.BDDfy;
using TestStack.BDDfy.Configuration;

namespace Specify.IntegrationTests.Containers.Ioc
{
    public class AutofacExamplesLifecycleTests : ExamplesLifecycle<AutofacContainer>
    {
        protected override AutofacContainer CreateSut()
        {
            Action<ContainerBuilder> registrations = builder =>
            {
                builder.RegisterType<UnitScenarioWithAllSupportedStepsInRandomOrderWithExamples>();
                builder.RegisterType<ExampleLifecycleAction1>().As<IPerScenarioAction>();
                builder.RegisterType<ExampleLifecycleAction2>().As<IPerScenarioAction>();
            };

            return ContainerFactory.CreateAutofacContainer<NullMockFactory>(registrations);
        }
    }
    public class DryExamplesLifecycleTests : ExamplesLifecycle<DryContainer>
    {
        protected override DryContainer CreateSut()
        {
            Action<Container> registrations = container =>
            {
                container.Register<UnitScenarioWithAllSupportedStepsInRandomOrderWithExamples>(Reuse.ScopedOrSingleton);
                container.Register<IPerScenarioAction, ExampleLifecycleAction1>(Reuse.ScopedOrSingleton);
                container.Register<IPerScenarioAction, ExampleLifecycleAction2>(Reuse.ScopedOrSingleton);
            };

            return ContainerFactory.CreateDryContainer<NullMockFactory>(registrations);
        }
    }
    public abstract class ExamplesLifecycle<T> where T : IContainer
    {
        protected abstract T CreateSut();
        private UnitScenarioWithAllSupportedStepsInRandomOrderWithExamples _scenario;

        public IContainer SUT { get; set; }

        [Test]
        public void specify_steps_should_wrap_standard_BDDfy_conventions_with_examples()
        {
            Configurator.Scanners.ExecutableAttributeScanner.Disable();
            Configurator.Scanners.Add(() => new SpecifyExecutableAttributeStepScanner());

            SUT = CreateSut();
            _scenario = SUT.Get<UnitScenarioWithAllSupportedStepsInRandomOrderWithExamples>();
            _scenario.SetContainer(SUT);

            _scenario
                .WithExamples(_scenario.Examples)
                .BDDfy(_scenario.Title);

            _scenario.Steps[0].ShouldBe("Constructor");

            for (int i = 0; i < _scenario.Examples.Count; i++)
            {
                _scenario.Steps[(i * 14) + 1].ShouldBe($"Begin test case {i + 1}");             // Specify begin test case method
                _scenario.Steps[(i * 14) + 2].ShouldBe("Action ExampleLifecycleAction1 - Before");
                _scenario.Steps[(i * 14) + 3].ShouldBe("Action ExampleLifecycleAction2 - Before");
                _scenario.Steps[(i * 14) + 4].ShouldBe("Setup");
                _scenario.Steps[(i * 14) + 5].ShouldBe("EstablishContext");
                _scenario.Steps[(i * 14) + 6].ShouldBe("GivenSomePrecondition");
                _scenario.Steps[(i * 14) + 7].ShouldBe("AndGivenSomeOtherPrecondition");
                _scenario.Steps[(i * 14) + 8].ShouldBe("WhenAction");
                _scenario.Steps[(i * 14) + 9].ShouldBe("AndWhenAnotherAction");
                _scenario.Steps[(i * 14) + 10].ShouldBe("ThenAnExpectation");
                _scenario.Steps[(i * 14) + 11].ShouldBe("AndThenAnotherExpectation");
                _scenario.Steps[(i * 14) + 12].ShouldBe("TearDown");
               // _scenario.Steps[(i *15) + 13].ShouldBe("EndTestCase");              // Specify end test case method
                _scenario.Steps[(i * 14) + 13].ShouldBe("Action ExampleLifecycleAction2 - After");
                _scenario.Steps[(i * 14) + 14].ShouldBe("Action ExampleLifecycleAction1 - After");
            }
        }

    }
    class ExampleLifecycleAction1 : ExampleLifecycleAction
    {
        public ExampleLifecycleAction1() : base(1) { }
    }
    class ExampleLifecycleAction2 : ExampleLifecycleAction
    {
        public ExampleLifecycleAction2() : base(2) { }
    }
    abstract class ExampleLifecycleAction : IPerScenarioAction
    {
        private UnitScenarioWithAllSupportedStepsInRandomOrderWithExamples _scenario;

        protected ExampleLifecycleAction(int order)
        {
            Order = order;
        }

        public void Before<TSut>(IScenario<TSut> scenario) where TSut : class
        {
            _scenario = (UnitScenarioWithAllSupportedStepsInRandomOrderWithExamples)scenario;
            if (Order == 1)
            {
                _scenario.Steps.Add($"Begin test case {_scenario.TestCaseNumber}");
            }
            _scenario.Steps.Add($"Action {GetType().Name} - Before");
        }

        public void After()
        {
            _scenario.Steps.Add($"Action {GetType().Name} - After");
        }

        public bool ShouldExecute(Type type)
        {
            return type.Name == "UnitScenarioWithAllSupportedStepsInRandomOrderWithExamples";
        }

        public int Order { get; set; }
    }

}
