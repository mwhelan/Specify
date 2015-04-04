//using NUnit.Framework;
//using Shouldly;
//using Specify.Containers;
//using Specify.Tests.Stubs;
//using TinyIoC;

//namespace Specify.Tests.Containers
//{
//    [TestFixture]
//    public class DefaultContainerTests
//    {
//        [Test]
//        public void can_resolve_concrete_types()
//        {
//            var sut = new DefaultContainer(new TinyIoCContainer());
//            sut.CanResolve<ConcreteObjectWithOneConcreteConstructor>().ShouldBe(true);
//            sut.CanResolve<Dependency1>().ShouldBe(true);
//        }

//        [Test]
//        public void cannot_resolve_service_implementations_that_are_not_registered()
//        {
//            var sut = new DefaultContainer(new TinyIoCContainer());
//            sut.CanResolve<ConcreteObjectWithOneInterfaceConstructor>().ShouldBe(false);
//        }

//        [Test]
//        public void can_register_service_implementation()
//        {
//            var sut = new DefaultContainer(new TinyIoCContainer());
//            sut.Register<IDependency1, Dependency1>();
//            sut.CanResolve<ConcreteObjectWithOneInterfaceConstructor>().ShouldBe(true);
//        }

//        [Test]
//        public void TinyIoc_RegisterService_should_register_singleton_lifetime()
//        {
//            var sut = new TinyIoCContainer();
//            sut.Register<IDependency2, Dependency2>();
//            sut.Resolve<IDependency2>().ShouldBeSameAs(sut.Resolve<IDependency2>());
//        }

//        [Test]
//        public void Default_RegisterService_should_register_singleton_lifetime()
//        {
//            var sut = new DefaultContainer();
//            sut.Register<IDependency2, Dependency2>();
//            sut.Resolve<IDependency2>().ShouldBeSameAs(sut.Resolve<IDependency2>());
//        }

//        [Test]
//        public void RegisterService_should_register_singleton_lifetime()
//        {
//            var sut = new SutFactory<ConcreteObjectWithMultipleConstructors>(new DefaultContainer());
//            sut.Register<IDependency2, Dependency2>();
//            sut.Get<IDependency2>().ShouldBeSameAs(sut.Get<IDependency2>());
//        }
//    }

    
//}
