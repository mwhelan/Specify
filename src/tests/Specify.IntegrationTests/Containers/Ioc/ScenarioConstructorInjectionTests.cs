using System;
using Autofac;
using DryIoc;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Autofac;
using Specify.Containers;
using Specify.Mocks;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Containers.Ioc
{
    public abstract class ScenarioConstructorInjectionTests<T> where T : IContainer
    {
        protected abstract T CreateSut();
        private ScenarioWithConstuctorParmeters _scenario;

        public IContainer SUT { get; set; }

        [SetUp]
        public void SetUp()
        {
            SUT = CreateSut();
            _scenario = SUT.Get<ScenarioWithConstuctorParmeters>();
            _scenario.SetContainer(SUT);
            _scenario.BeginTestCase();
        }

        [Test]
        public void DependencyIsInjected()
        {
            _scenario.Dependency1.ShouldNotBeNull();
            _scenario.Dependency2.ShouldNotBeNull();
        }

        [Test]
        public void DependencyIsSharedOnAPerTestBasis()
        {
            _scenario.Dependency1.ShouldBeSameAs(_scenario.SUT.Dependency1);
        }
    }

    public class AutofacScenarioConstructorInjectionTests : ScenarioConstructorInjectionTests<AutofacContainer>
    {
        protected override AutofacContainer CreateSut()
        {
            Action<ContainerBuilder> registrations = builder =>
            {
                builder.RegisterType<ScenarioWithConstuctorParmeters>();
                builder.RegisterType<ConcreteObjectWithOneInterfaceConstructor>();
                builder.RegisterType<Dependency1>().As<IDependency1>().SingleInstance();
                builder.Register(c => Substitute.For<IDependency2>()).As<IDependency2>();
            };

            return ContainerFactory.CreateAutofacContainer<NullMockFactory>(registrations);
        }
    }

    public class DryScenarioConstructorInjectionTests : ScenarioConstructorInjectionTests<DryContainer>
    {
        protected override DryContainer CreateSut()
        {
            Action<Container> registrations = container =>
            {
                container.Register<ScenarioWithConstuctorParmeters>(Reuse.Singleton);
                container.Register<ConcreteObjectWithOneInterfaceConstructor>(Reuse.Singleton);
                container.Register<IDependency1, Dependency1>(Reuse.Singleton);
                container.RegisterDelegate<IDependency2>(r => Substitute.For<IDependency2>(), Reuse.Singleton);
            };

            return ContainerFactory.CreateDryContainer<NullMockFactory>(registrations);
        }
    }

    public class ScenarioWithConstuctorParmeters : ScenarioFor<ConcreteObjectWithOneInterfaceConstructor>
    {
        public IDependency1 Dependency1 { get; }
        public IDependency2 Dependency2 { get; }

        public ScenarioWithConstuctorParmeters(IDependency1 dependency1, IDependency2 dependency2)
        {
            Dependency1 = dependency1;
            Dependency2 = dependency2;
        }
    }
}