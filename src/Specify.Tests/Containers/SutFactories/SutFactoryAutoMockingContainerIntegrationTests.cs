using Specify.Autofac;
using Specify.Mocks;

namespace Specify.Tests.Containers.SutFactories
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
