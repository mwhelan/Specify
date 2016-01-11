using Specify.Configuration;
using Specify.Mocks;

namespace Specify.IntegrationTests.Containers.AutoMocking
{
    public abstract class LightMockingContainerTestsFor<TMockFactory> : MockingContainerTestsFor<LightContainer>
        where TMockFactory : IMockFactory
    {
        protected override LightContainer CreateSut()
        {
            var mockFactoryInstance = typeof(TMockFactory).Create<IMockFactory>();
            var serviceContainer = new LightContainerFactory().Create(() => mockFactoryInstance);
            var container = (LightContainer)serviceContainer.GetInstance<IContainer>();
            return container;
        }
    }

    public class LightNSubstituteContainerForIntegrationTests
        : LightMockingContainerTestsFor<NSubstituteMockFactory> { }

    public class LightMoqContainerForIntegrationTests
        : LightMockingContainerTestsFor<MoqMockFactory> { }

    public class LightFakeItEasyContainerForIntegrationTests
        : LightMockingContainerTestsFor<FakeItEasyMockFactory> { }
}
