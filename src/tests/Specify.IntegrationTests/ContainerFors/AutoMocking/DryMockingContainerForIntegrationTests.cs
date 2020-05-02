using DryIoc;
using Specify.Containers;
using Specify.Mocks;

namespace Specify.IntegrationTests.ContainerFors.AutoMocking
{
    public abstract class DryMockingContainerForIntegrationTests<TMockFactory> : ContainerForIntegrationTestsBase
        where TMockFactory : IMockFactory
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            var mockFactoryInstance = typeof(TMockFactory).Create<IMockFactory>();
            var container = new DryContainerFactory().Create(mockFactoryInstance);
            var mockingContainer = container.Resolve<IContainer>() as DryMockingContainer;
            return new ContainerFor<T>(mockingContainer);
        }
    }

    public class DryNSubstituteContainerForIntegrationTests
        : DryMockingContainerForIntegrationTests<NSubstituteMockFactory> { }

    public class DryMoqContainerForIntegrationTests 
        : DryMockingContainerForIntegrationTests<MoqMockFactory> { }

    public class DryFakeItEasyContainerForIntegrationTests
        : DryMockingContainerForIntegrationTests<FakeItEasyMockFactory> { }
}
