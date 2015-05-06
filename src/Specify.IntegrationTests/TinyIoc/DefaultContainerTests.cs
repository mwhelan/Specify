namespace Specify.IntegrationTests.TinyIoc
{
    using NUnit.Framework;

    using Shouldly;

    using Specify.Tests.Stubs;

    public class DefaultContainerTests : IocContainerTests<DefaultContainer>
    {
        protected override DefaultContainer CreateSut()
        {
            return new DefaultContainer();
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