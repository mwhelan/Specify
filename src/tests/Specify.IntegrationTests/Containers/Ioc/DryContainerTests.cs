using System;
using DryIoc;
using Specify.Containers;
using Specify.Mocks;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Containers.Ioc
{
    public class DryContainerGetTests : IocContainerGetTestsFor<DryContainer>
    {
        protected override DryContainer CreateSut()
        {
            Action<Container> registrations = container => container.Register<ConcreteObjectWithOneInterfaceConstructor>();
            return ContainerFactory.CreateDryContainer<NullMockFactory>(registrations);
        }
    }

    public class DryContainerSetTests : IocContainerSetTestsFor<DryContainer>
    {
        protected override DryContainer CreateSut()
        {
            Action<Container> registrations = container => container.Register<ConcreteObjectWithOneInterfaceConstructor>();
            return ContainerFactory.CreateDryContainer<NullMockFactory>(registrations);
        }
    }

}
