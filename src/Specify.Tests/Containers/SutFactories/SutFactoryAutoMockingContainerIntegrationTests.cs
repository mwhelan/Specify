using Specify.Containers;
using Specify.Containers.Mocking;

namespace Specify.Tests.Containers.SutFactory
{
    public class SutFactoryAutoMockingContainerIntegrationTests : SutFactoryIntegrationTests
    {
        protected override SutFactory<T> CreateSut<T>()
        {
            var container = new AutoMockingContainer(new NSubstituteMockFactory());
            return new SutFactory<T>(container);
        }
    }
}
