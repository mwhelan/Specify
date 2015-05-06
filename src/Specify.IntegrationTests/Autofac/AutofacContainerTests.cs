using NUnit.Framework;
using Shouldly;
using Specify.Autofac;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Autofac
{
    public class AutofacContainerTests : IocContainerTests<AutofacScenarioContainer>
    {
        protected override AutofacScenarioContainer CreateSut()
        {
            return new AutofacScenarioContainer();
        }

        [Test]
        public void cannot_resolve_concrete_types_not_registered()
        {
            var sut = this.CreateSut();
            sut.CanResolve<ConcreteObjectWithOneConcreteConstructor>().ShouldBe(false);
            sut.CanResolve<Dependency1>().ShouldBe(false);
        }
    }
}