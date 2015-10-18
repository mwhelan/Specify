using Specify.Autofac;
using Specify.Examples.Autofac;
using Specify.Mocks;

namespace Specify.IntegrationTests.Autofac
{
    public class SutFactoryAutofacFakeItEasyContainerIntegrationTests : SutFactoryIntegrationTests
    {
        protected override SutFactory<T> CreateSut<T>()
        {
            var container = new AutofacApplicationContainer { MockFactory = () => new FakeItEasyMockFactory() };
            return new SutFactory<T>(container.CreateChildContainer());
        }
    }
}
