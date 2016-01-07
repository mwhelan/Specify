using NUnit.Framework;
using Shouldly;
using Specify.Autofac;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Containers.Ioc
{
    public class AutofacContainerTests : IocContainerTestsFor<AutofacContainer>
    {
        protected override AutofacContainer CreateSut()
        {
            return new AutofacContainer();
        }

        [Test]
        public void cannot_resolve_concrete_types_not_registered()
        {
            var sut = this.CreateSut();
            sut.CanGet<ConcreteObjectWithOneConcreteConstructor>().ShouldBe(false);
            sut.CanGet<Dependency1>().ShouldBe(false);
        }
    }
}