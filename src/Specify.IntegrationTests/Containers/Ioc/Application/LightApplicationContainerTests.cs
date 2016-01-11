using Specify.Configuration;

namespace Specify.IntegrationTests.Containers.Ioc.Application
{
    public class LightApplicationContainerTests : ApplicationContainerTestsFor<LightContainer>
    {
        protected override LightContainer CreateSut()
        {
            var serviceContainer = new LightContainerFactory().Create(null);
            return new LightContainer(serviceContainer, null);
        }
    }
}
