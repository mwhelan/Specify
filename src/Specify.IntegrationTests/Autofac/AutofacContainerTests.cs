namespace Specify.IntegrationTests.Autofac
{
    using NUnit.Framework;

    using Shouldly;

    using Specify.Autofac;
    using Specify.Tests.Stubs;

    public class AutofacContainerTests : IocContainerTests<AutofacContainer>
    {
        protected override AutofacContainer CreateSut()
        {
            return new AutofacContainer();
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