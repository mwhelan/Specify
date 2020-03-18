using System;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Exceptions;
using Specify.Tests.Stubs;

namespace Specify.Tests.SpecifyRootFolder
{
    [TestFixture]
    public class ContainerForTests
    {
        [Test]
        public void should_use_container_to_create_sut()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            var result = sut.SystemUnderTest;
            sut.SourceContainer.Received<IContainer>().Get<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void sut_should_be_singleton()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();

            var result = sut.SystemUnderTest;

            sut.SystemUnderTest.ShouldBeSameAs(result);
            sut.SourceContainer.Received<IContainer>(1).Get<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void should_be_able_to_set_sut_independently()
        {
            var instance = new ConcreteObjectWithNoConstructor();
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            var original = sut.SystemUnderTest;

            sut.SystemUnderTest = instance;

            sut.SystemUnderTest.ShouldBeSameAs(instance);
            sut.SystemUnderTest.ShouldNotBeSameAs(original);
        }

        [Test]
        public void Get_generic_should_resolve_service()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            sut.Get<ConcreteObjectWithNoConstructor>();
            sut.SourceContainer.Received<IContainer>().Get<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void Get_generic_should_throw_InterfaceResolutionException_if_container_throws()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            sut.SourceContainer.Get<ConcreteObjectWithNoConstructor>(null)
                .Returns(x => { throw new Exception(); });

            Action action = () => sut.Get<ConcreteObjectWithNoConstructor>();

            Should.Throw<InterfaceResolutionException>(action)
                .Message.ShouldStartWith("Failed to resolve an implementation of Specify.Tests.Stubs.ConcreteObjectWithNoConstructor.");
        }

        [Test]
        public void Get_should_resolve_service()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            sut.Get(typeof(ConcreteObjectWithNoConstructor));
            sut.SourceContainer.Received<IContainer>().Get(typeof(ConcreteObjectWithNoConstructor));
        }

        [Test]
        public void Get_should_throw_InterfaceResolutionException_if_container_throws()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            sut.SourceContainer.Get(typeof (ConcreteObjectWithNoConstructor), null)
                .Returns(x => { throw new Exception(); });

            Action action = () => sut.Get(typeof(ConcreteObjectWithNoConstructor));

            Should.Throw<InterfaceResolutionException>(action)
                .Message.ShouldStartWith("Failed to resolve an implementation of Specify.Tests.Stubs.ConcreteObjectWithNoConstructor.");
        }

        [Test]
        public void CanResolve_generic_should_call_container_CanResolve_generic()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            sut.CanResolve<ConcreteObjectWithNoConstructor>();
            sut.SourceContainer.Received<IContainer>().CanResolve<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void CanResolve_should_call_container_CanResolve()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            sut.CanResolve(typeof(ConcreteObjectWithNoConstructor));
            sut.SourceContainer.Received<IContainer>().CanResolve(typeof(ConcreteObjectWithNoConstructor));
        }

        [Test]
        public void should_dispose_container_when_disposed()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            sut.Dispose();
            sut.SourceContainer.Received<IContainer>().Dispose();
        }

        private ContainerFor<T> CreateSut<T>() where T : class
        {
            return new ContainerFor<T>(Substitute.For<IContainer>());
        }
    }
}
