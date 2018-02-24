using DryIoc;
using Specify.Microsoft.DependencyInjection;
using Specify.Mocks;

namespace Specify.IntegrationTests.Containers.AutoMocking
{
    public abstract class DryMockingContainerTestsFor<TMockFactory> : MockingContainerTestsFor<DryMockingContainer>
        where TMockFactory : IMockFactory
    {
        protected override DryMockingContainer CreateSut()
        {
            var mockFactoryInstance = typeof(TMockFactory).Create<IMockFactory>();
            var container = new DryMockingContainer(mockFactoryInstance, new Container(rules => rules.WithConcreteTypeDynamicRegistrations()));
            return container;
        }
    }

    public class DryNSubstituteContainerForIntegrationTests
        : DryMockingContainerTestsFor<NSubstituteMockFactory>
    { }

    public class DryMoqContainerForIntegrationTests
        : DryMockingContainerTestsFor<MoqMockFactory>
    { }

    public class DryFakeItEasyContainerForIntegrationTests
        : DryMockingContainerTestsFor<FakeItEasyMockFactory>
    { }
}
