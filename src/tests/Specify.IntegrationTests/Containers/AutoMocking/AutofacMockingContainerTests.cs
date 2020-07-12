using System;
using Autofac;
using Specify.Autofac;
using Specify.Mocks;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Containers.AutoMocking
{
    public abstract class AutofacMockingContainerTestsFor<TMockFactory> : MockingContainerTestsFor<AutofacContainer>
        where TMockFactory : IMockFactory
    {
        protected override AutofacContainer CreateSut()
        {
            Action<ContainerBuilder> registrations = builder => builder.RegisterType<ConcreteObjectWithOneInterfaceConstructor>();
            return ContainerFactory.CreateAutofacContainer<TMockFactory>(registrations);
        }
    }

    public class AutofacNSubstituteContainerForIntegrationTests
        : AutofacMockingContainerTestsFor<NSubstituteMockFactory> { }

    public class AutofacMoqContainerForIntegrationTests
        : AutofacMockingContainerTestsFor<MoqMockFactory> { }

    public class AutofacFakeItEasyContainerForIntegrationTests
        : AutofacMockingContainerTestsFor<FakeItEasyMockFactory> { }
}
