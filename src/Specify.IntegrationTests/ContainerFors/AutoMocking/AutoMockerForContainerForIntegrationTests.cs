using Specify.Mocks;

namespace Specify.IntegrationTests.ContainerFors.AutoMocking
{
    public class AutoMockerForContainerForIntegrationTests : ContainerForIntegrationTestsBase
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            return new AutoMockerFor<T>();
        }
    }
}