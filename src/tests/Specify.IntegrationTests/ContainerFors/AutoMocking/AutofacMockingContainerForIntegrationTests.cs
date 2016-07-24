using Autofac;
using Autofac.Features.ResolveAnything;
using Specify.Autofac;
using Specify.Mocks;

namespace Specify.IntegrationTests.ContainerFors.AutoMocking
{
    public abstract class AutofacMockingContainerForIntegrationTests<TMockFactory> : ContainerForIntegrationTestsBase
        where TMockFactory : IMockFactory
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            var mockFactoryInstance = typeof(TMockFactory).Create<IMockFactory>();
            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            builder.RegisterSource(new AutofacMockRegistrationHandler(mockFactoryInstance));
            var container = builder.Build();
            return new ContainerFor<T>(new AutofacContainer(container));
        }
    }

    public class AutofacNSubstituteContainerForIntegrationTests
        : AutofacMockingContainerForIntegrationTests<NSubstituteMockFactory> { }

    public class AutofacMoqContainerForIntegrationTests
        : AutofacMockingContainerForIntegrationTests<MoqMockFactory> { }

    public class AutofacFakeItEasyContainerForIntegrationTests
        : AutofacMockingContainerForIntegrationTests<FakeItEasyMockFactory> { }
}