using Ninject;
using NUnit.Framework;
using Shouldly;
using Specify.Ninject;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Ninject
{
    public class NinjectContainerTests : IocContainerTests<NinjectScenarioContainer>
    {
        protected override NinjectScenarioContainer CreateSut()
        {
            return new NinjectScenarioContainer(new StandardKernel());
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