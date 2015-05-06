using Specify.Autofac;
using Specify.Mocks;

namespace Specify.IntegrationTests.Autofac
{
    public class SutFactoryAutoMockingContainerIntegrationTests : SutFactoryIntegrationTests
    {
        protected override SutFactory<T> CreateSut<T>()
        {
            var container = new AutofacAutoMockingContainer(new NSubstituteMockFactory());
            return new SutFactory<T>(container);
        }
    }
}
