using Autofac;
using Autofac.Features.ResolveAnything;
using Specify.Autofac;
using Specify.Mocks;

namespace Specify.IntegrationTests.Containers.AutoMocking
{
    public abstract class AutofacMockingContainerTestsFor<TMockFactory> : MockingContainerTestsFor<AutofacContainer>
        where TMockFactory : IMockFactory
    {
        protected override AutofacContainer CreateSut()
        {
            var mockFactoryInstance = typeof(TMockFactory).Create<IMockFactory>();
            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            builder.RegisterSource(new AutofacMockRegistrationHandler(mockFactoryInstance));
            var container = builder.Build();
            return new AutofacContainer(container);
        }
    }

    public class AutofacNSubstituteContainerForIntegrationTests
        : AutofacMockingContainerTestsFor<NSubstituteMockFactory> { }

    public class AutofacMoqContainerForIntegrationTests
        : AutofacMockingContainerTestsFor<MoqMockFactory> { }

    public class AutofacFakeItEasyContainerForIntegrationTests
        : AutofacMockingContainerTestsFor<FakeItEasyMockFactory> { }
}
