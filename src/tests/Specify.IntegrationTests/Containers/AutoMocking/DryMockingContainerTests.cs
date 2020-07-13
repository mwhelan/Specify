using DryIoc;
using Specify.Containers;
using Specify.Mocks;

namespace Specify.IntegrationTests.Containers.AutoMocking
{
    public abstract class DryMockingContainerGetTestsFor<TMockFactory> : MockingContainerGetTestsFor<DryMockingContainer>
        where TMockFactory : IMockFactory
    {
        protected override DryMockingContainer CreateSut()
        {
            var mockFactoryInstance = typeof(TMockFactory).Create<IMockFactory>();
            var container = new DryContainerFactory().Create(mockFactoryInstance);
            var mockingContainer = container.Resolve<IContainer>() as DryMockingContainer;
            return mockingContainer;
        }

        public override void CanResolve_should_return_false_if_service_not_registered_and_dependency_chains_are_not_resolvable()
        {
            // TinyIoc and Autofac containers could not resolve these types but DryIoc can
        }

        public override void Get_should_not_resolve_for_any_concrete_types_where_dependency_chains_are_not_resolvable()
        {
            // TinyIoc and Autofac containers could not resolve these types but DryIoc can
        }
    }

    public class DryNSubstituteMockingContainerGetTests
        : DryMockingContainerGetTestsFor<NSubstituteMockFactory>
    { }

    public class DryMoqMockingContainerGetTests
        : DryMockingContainerGetTestsFor<MoqMockFactory>
    { }

    public class DryFakeItEasyMockingContainerGetTests
        : DryMockingContainerGetTestsFor<FakeItEasyMockFactory>
    { }

    public abstract class DryMockingContainerSetTestsFor<TMockFactory> : MockingContainerSetTestsFor<DryMockingContainer>
        where TMockFactory : IMockFactory
    {
        protected override DryMockingContainer CreateSut()
        {
            var mockFactoryInstance = typeof(TMockFactory).Create<IMockFactory>();
            var container = new DryContainerFactory().Create(mockFactoryInstance);
            var mockingContainer = container.Resolve<IContainer>() as DryMockingContainer;
            return mockingContainer;
        }
    }

    public class DryNSubstituteMockingContainerSetTests
        : DryMockingContainerSetTestsFor<NSubstituteMockFactory>
    { }

    public class DryMoqMockingContainerSetTests
        : DryMockingContainerSetTestsFor<MoqMockFactory>
    { }

    public class DryFakeItEasyMockingContainerSetTests
        : DryMockingContainerSetTestsFor<FakeItEasyMockFactory>
    { }
}