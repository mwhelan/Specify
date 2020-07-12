using Autofac;
using Specify.Autofac;
using Specify.Mocks;

namespace Specify.IntegrationTests.Containers.AutoMocking
{
    public abstract class AutofacMockingContainerTestsFor<TMockFactory> : MockingContainerTestsFor<AutofacContainer>
        where TMockFactory : IMockFactory
    {
        protected override AutofacContainer CreateSut()
        {
            return ContainerFactory.CreateAutofacContainer<TMockFactory>();
        }
    }

    public class AutofacNSubstituteContainerForIntegrationTests
        : AutofacMockingContainerTestsFor<NSubstituteMockFactory> { }

    public class AutofacMoqContainerForIntegrationTests
        : AutofacMockingContainerTestsFor<MoqMockFactory> { }

    public class AutofacFakeItEasyContainerForIntegrationTests
        : AutofacMockingContainerTestsFor<FakeItEasyMockFactory> { }
}
