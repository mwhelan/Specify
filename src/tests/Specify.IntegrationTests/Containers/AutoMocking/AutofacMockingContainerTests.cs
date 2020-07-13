using System;
using Autofac;
using Specify.Autofac;
using Specify.Mocks;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Containers.AutoMocking
{
    public abstract class AutofacMockingContainerTestsFor<TMockFactory> : MockingContainerGetTestsFor<AutofacContainer>
        where TMockFactory : IMockFactory
    {
        protected override AutofacContainer CreateSut()
        {
            Action<ContainerBuilder> registrations = builder => builder.RegisterType<ConcreteObjectWithOneInterfaceConstructor>();
            return ContainerFactory.CreateAutofacContainer<TMockFactory>(registrations);
        }
    }

    public class AutofacNSubstituteMockingContainerGetTests
        : AutofacMockingContainerTestsFor<NSubstituteMockFactory> { }

    public class AutofacMoqMockingContainerGetTests
        : AutofacMockingContainerTestsFor<MoqMockFactory> { }

    public class AutofacFakeItEasyMockingContainerGetTests
        : AutofacMockingContainerTestsFor<FakeItEasyMockFactory> { }
}
