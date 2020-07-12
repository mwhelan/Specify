using System;
using Autofac;
using Specify.Autofac;
using Specify.Mocks;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Containers.Ioc.Application
{
    public class AutofacApplicationContainerTests : ApplicationContainerTestsFor<AutofacContainer>
    {
        protected override AutofacContainer CreateSut()
        {
            Action<ContainerBuilder> registrations = builder => builder.RegisterType<ConcreteObjectWithOneInterfaceConstructor>();
            return ContainerFactory.CreateAutofacContainer<NullMockFactory>(registrations);
        }
    }
}