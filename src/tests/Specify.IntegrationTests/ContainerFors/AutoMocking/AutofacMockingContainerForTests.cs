using Specify.IntegrationTests.Containers;
using Specify.Mocks;

namespace Specify.IntegrationTests.ContainerFors.AutoMocking
{
    public abstract class AutofacMockingContainerForGetTests<TMockFactory> : ContainerForGetTestsBase
        where TMockFactory : IMockFactory
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            var container = ContainerFactory.CreateAutofacContainer<TMockFactory>();
            return new ContainerFor<T>(container);
        }
    }

    public class AutofacNSubstituteContainerForGetTests
        : AutofacMockingContainerForGetTests<NSubstituteMockFactory> { }

    public class AutofacMoqContainerForGetTests
        : AutofacMockingContainerForGetTests<MoqMockFactory> { }

    public class AutofacFakeItEasyContainerForGetTests
        : AutofacMockingContainerForGetTests<FakeItEasyMockFactory> { }

}