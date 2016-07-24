using Specify.Mocks;
using TinyIoC;

namespace Specify.IntegrationTests.ContainerFors.AutoMocking
{
    public abstract class TinyMockingContainerForIntegrationTests<TMockFactory> : ContainerForIntegrationTestsBase
        where TMockFactory : IMockFactory
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            var mockFactoryInstance = typeof (TMockFactory).Create<IMockFactory>();
            var container = new TinyMockingContainer(mockFactoryInstance, new TinyIoCContainer());
            return new ContainerFor<T>(container);
        }
    }

    public class TinyNSubstituteContainerForIntegrationTests
        : TinyMockingContainerForIntegrationTests<NSubstituteMockFactory> { }

    public class TinyMoqContainerForIntegrationTests 
        : TinyMockingContainerForIntegrationTests<MoqMockFactory> { }

    public class TinyFakeItEasyContainerForIntegrationTests
        : TinyMockingContainerForIntegrationTests<FakeItEasyMockFactory> { }
}
