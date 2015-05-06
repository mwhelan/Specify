namespace Specify.IntegrationTests.Ninject
{
    using global::Ninject;

    using NUnit.Framework;

    using Shouldly;

    using Specify.Ninject;
    using Specify.Tests.Stubs;

    public class NinjectContainerTests : IocContainerTests<NinjectContainer>
    {
        protected override NinjectContainer CreateSut()
        {
            return new NinjectContainer(new StandardKernel());
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