using System;
using Autofac;
using Specify.Autofac;
using Specify.IntegrationTests.Containers;
using Specify.Mocks;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.ContainerFors.Ioc
{
    public class AutofacContainerForGetTests : ContainerForGetTestsBase
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            Action<ContainerBuilder> registrations = builder =>
            {
                builder.RegisterType<Dependency4>().As<IDependency3>();
                builder.RegisterType<Dependency3>().As<IDependency3>();

                builder.RegisterType<Dependency1>().As<IDependency1>();
                builder.RegisterType<Dependency2>().As<IDependency2>();
                builder.RegisterType<ConcreteObjectWithNoConstructor>();
                builder.RegisterType<ConcreteObjectWithMultipleConstructors>();
                builder.RegisterType<ConcreteObjectWithOneInterfaceCollectionConstructor>();
            };

            var container = ContainerFactory.CreateAutofacContainer<NullMockFactory>(registrations);
            
            return new ContainerFor<T>(container);
        }
    }
}