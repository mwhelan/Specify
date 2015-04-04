using Specify.Containers;
using Specify.Tests.Containers;
using Specify.Tests.Containers.SutFactory;

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