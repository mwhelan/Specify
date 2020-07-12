using System;
using DryIoc;
using Specify.Containers;
using Specify.Mocks;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Containers.Ioc
{
    public class DryContainerTests : IocContainerTestsFor<DryContainer>
    {
        protected override DryContainer CreateSut()
        {
            Action<Container> registrations = container => container.Register<ConcreteObjectWithOneInterfaceConstructor>();
            return ContainerFactory.CreateDryContainer<NullMockFactory>(registrations);
        }
    }
}
