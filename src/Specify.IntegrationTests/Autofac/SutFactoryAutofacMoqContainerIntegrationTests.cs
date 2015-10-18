using Specify.Autofac;
using Specify.Mocks;

namespace Specify.IntegrationTests.Autofac
{
    public class SutFactoryAutofacMoqContainerIntegrationTests : SutFactoryIntegrationTests
    {
        protected override SutFactory<T> CreateSut<T>()
        {
            var container = new AutofacApplicationContainer { MockFactory = () => new MoqMockFactory() };
            return new SutFactory<T>(container.CreateChildContainer());
        }
    }
}