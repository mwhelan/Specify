using Specify.Containers;
using Specify.Examples.Autofac;
using Specify.Tests.Containers;

namespace Specify.Tests.Examples
{
    public class SutFactoryAutofacMoqContainerIntegrationTests : SutFactoryIntegrationTests
    {
        protected override SutFactory<T> CreateSut<T>()
        {
            return new SutFactory<T>(new AutofacMoqContainer());
        }
    }
}