using Specify.Autofac;
using Specify.Mocks;
using Specify.Tests.Containers;
using Specify.Tests.Containers.SutFactories;

namespace Specify.Tests.Examples
{
    public class SutFactoryAutofacNSubstituteContainerIntegrationTests : SutFactoryIntegrationTests
    {
        protected override SutFactory<T> CreateSut<T>()
        {
            var container = new AutoMockingContainer(new NSubstituteMockFactory());
            return new SutFactory<T>(container);
        }
    }
}