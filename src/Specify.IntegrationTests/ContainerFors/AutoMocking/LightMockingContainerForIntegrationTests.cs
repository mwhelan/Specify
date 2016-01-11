using Specify.Configuration;
using Specify.Mocks;

namespace Specify.IntegrationTests.ContainerFors.AutoMocking
{
    public abstract class LightMockingContainerForIntegrationTests<TMockFactory> : ContainerForIntegrationTestsBase
        where TMockFactory : IMockFactory
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            var mockFactoryInstance = typeof(TMockFactory).Create<IMockFactory>();
            var serviceContainer = new LightContainerFactory().Create(() => mockFactoryInstance);
            var container = (LightContainer)serviceContainer.GetInstance<IContainer>();
            return new ContainerFor<T>(container);
        }
    }

    public class LightNSubstituteContainerForIntegrationTests
        : LightMockingContainerForIntegrationTests<NSubstituteMockFactory> { }

    public class LightMoqContainerForIntegrationTests
        : LightMockingContainerForIntegrationTests<MoqMockFactory> { }

    public class LightFakeItEasyContainerForIntegrationTests
        : LightMockingContainerForIntegrationTests<FakeItEasyMockFactory> { }
}