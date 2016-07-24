using Specify.Mocks;

namespace Specify.IntegrationTests.ContainerFors.AutoMocking
{
    public class AutoMockerContainerForIntegrationTests : ContainerForIntegrationTestsBase
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            return new AutoMockerFor<T>();
        }
    }
}