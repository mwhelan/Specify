using NUnit.Framework;
using Shouldly;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.TinyIoc
{
    public class DefaultContainerTests : IocContainerTests<DefaultScenarioContainer>
    {
        protected override DefaultScenarioContainer CreateSut()
        {
            return new DefaultScenarioContainer();
        }

        [Test]
        public void can_resolve_concrete_types_not_registered()
        {
            var sut = this.CreateSut();
            sut.CanResolve<ConcreteObjectWithOneConcreteConstructor>().ShouldBe(true);
            sut.CanResolve<Dependency1>().ShouldBe(true);
        }


    }
}