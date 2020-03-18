using Autofac;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Autofac;
using Specify.Configuration.Examples;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Containers.Ioc
{
    public abstract class ScenarioConstructorInjectionTests<T> where T : IContainerRoot
    {
        protected abstract T CreateSut();
        private ScenarioWithConstuctorParmeters _scenario;

        public IContainerRoot SUT { get; set; }

        [SetUp]
        public void SetUp()
        {
            SUT = CreateSut();
            _scenario = SUT.Get<ScenarioWithConstuctorParmeters>();
            _scenario.SetTestScope(SUT.Get<TestScope>());
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
            var builder = IocTestHelpers.InitializeAutofaContainerBuilder();
            
            builder.RegisterType<ScenarioWithConstuctorParmeters>();
            builder.RegisterType<ConcreteObjectWithOneInterfaceConstructor>();
            builder.RegisterType<Dependency1>().As<IDependency1>().SingleInstance();
            builder.Register(c => Substitute.For<IDependency2>()).As<IDependency2>();
            return new AutofacContainer(builder);
        }
    }

    public class TinyScenarioConstructorInjectionTests : ScenarioConstructorInjectionTests<TinyContainer>
    {
        protected override TinyContainer CreateSut()
        {
            var builder = IocTestHelpers.InitializeTinyIoCContainer();
            builder.Register<ScenarioWithConstuctorParmeters>();
            builder.Register<ConcreteObjectWithOneInterfaceConstructor>();
            builder.Register<IDependency1, Dependency1>().AsSingleton();
            builder.Register<IDependency2>((c, p) => Substitute.For<IDependency2>());
            return new TinyContainer(builder);
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