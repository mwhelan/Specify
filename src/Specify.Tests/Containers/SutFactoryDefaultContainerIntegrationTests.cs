using Specify.Containers;

namespace Specify.Tests.Containers
{
    public class SutFactoryDefaultContainerIntegrationTests : SutFactoryIntegrationTests
    {
        protected override SutFactory<T> CreateSut<T>() 
        {
            return new SutFactory<T>(new DefaultContainer());
        }
    }
}