using Specify.Mocks;

namespace Specify.IntegrationTests.ContainerFors.AutoMocking
{
    public class AutoMockerContainerForGetTests : ContainerForGetTestsBase
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            return new AutoMockerFor<T>();
        }
    }

    public class AutoMockerContainerForSetTests : ContainerForSetTestsBase
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            return new AutoMockerFor<T>();
        }
    }
}