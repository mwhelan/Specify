using Specify.Mocks;
using TinyIoC;

namespace Specify.IntegrationTests.Containers.AutoMocking
{
    public abstract class TinyMockingContainerTestsFor<TMockFactory> : MockingContainerTestsFor<TinyMockingContainer>
        where TMockFactory : IMockFactory
    {
        protected override TinyMockingContainer CreateSut()
        {
            var mockFactoryInstance = typeof (TMockFactory).Create<IMockFactory>();
            var container = new TinyMockingContainer(mockFactoryInstance, new TinyIoCContainer());
            return container;
        }
    }

    public class TinyNSubstituteContainerForIntegrationTests
        : TinyMockingContainerTestsFor<NSubstituteMockFactory> { }

    public class TinyMoqContainerForIntegrationTests
        : TinyMockingContainerTestsFor<MoqMockFactory> { }

    public class TinyFakeItEasyContainerForIntegrationTests
        : TinyMockingContainerTestsFor<FakeItEasyMockFactory> { }
}
