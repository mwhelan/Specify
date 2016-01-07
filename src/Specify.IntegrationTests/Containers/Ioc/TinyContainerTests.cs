using NUnit.Framework;
using Shouldly;
using Specify.Configuration;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Containers.Ioc
{
    public class TinyContainerTests : IocContainerTestsFor<TinyContainer>
    {
        protected override TinyContainer CreateSut()
        {
            var container = new TinyContainerFactory().Create(null);
            return new TinyContainer(container);
        }

        [Test]
        public void can_resolve_concrete_types_not_registered()
        {
            var sut = this.CreateSut();
            sut.CanGet<ConcreteObjectWithOneConcreteConstructor>().ShouldBe(true);
            sut.CanGet<Dependency1>().ShouldBe(true);
        }
    }
}