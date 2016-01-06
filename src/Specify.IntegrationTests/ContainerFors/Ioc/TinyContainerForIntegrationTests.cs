﻿using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.ContainerFors.Ioc
{
    public class TinyContainerForIntegrationTests : ContainerForIntegrationTestsBase
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            var container = new TinyContainer();
            container.Register<IDependency1, Dependency1>();
            container.Register<IDependency2, Dependency2>();
            container.Container.RegisterMultiple<IDependency3>(new []{typeof(Dependency3), typeof(Dependency4)});
            container.Register<ConcreteObjectWithNoConstructor>();
            container.Register<ConcreteObjectWithMultipleConstructors>();
            container.Register<ConcreteObjectWithOneInterfaceCollectionConstructor>();
            return new ContainerFor<T>(container);
        }
    }
    //public class SutFactoryTinyNSubstituteContainerIntegrationTests : SutFactoryIntegrationTests
    //{
    //    protected override ContainerFor<T> CreateSut<T>()
    //    {
    //        var container = new TinyAutoMockingContainer(new NSubstituteMockFactory());
    //        return new ContainerFor<T>(container);
    //    }
    //}
}
