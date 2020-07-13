using DryIoc;
using Specify.Containers;
using Specify.Mocks;

namespace Specify.IntegrationTests.ContainerFors.AutoMocking
{
    public abstract class DryMockingContainerForGetTests<TMockFactory> : ContainerForGetTestsBase
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

    public class DryNSubstituteContainerForGetTests
        : DryMockingContainerForGetTests<NSubstituteMockFactory> { }

    public class DryMoqContainerForGetTests 
        : DryMockingContainerForGetTests<MoqMockFactory> { }

    public class DryFakeItEasyContainerForGetTests
        : DryMockingContainerForGetTests<FakeItEasyMockFactory> { }



    public abstract class DryMockingContainerForSetTests<TMockFactory> : ContainerForSetTestsBase
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

    public class DryNSubstituteContainerForSetTests
        : DryMockingContainerForSetTests<NSubstituteMockFactory>
    { }

    public class DryMoqContainerForSetTests
        : DryMockingContainerForSetTests<MoqMockFactory>
    { }

    public class DryFakeItEasyContainerForSetTests
        : DryMockingContainerForSetTests<FakeItEasyMockFactory>
    { }
}
