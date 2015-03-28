using Specify.Containers;

namespace Specify.Tests.Containers
{
    public class SpecificationContext_DefaultContainer_IntegrationTests : SpecificationContextIntegrationTests
    {
        protected override SpecificationContext<T> CreateSut<T>() 
        {
            return new SpecificationContext<T>(new DefaultContainer());
        }
    }
}