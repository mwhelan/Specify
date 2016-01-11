using Specify.Configuration;

namespace Specify.IntegrationTests.Containers.Ioc
{
    public class LightContainerTests : IocContainerTestsFor<LightContainer>
    {
        protected override LightContainer CreateSut()
        {
            var serviceContainer = new LightContainerFactory().Create(null);
            return (LightContainer)serviceContainer.GetInstance<IContainer>();
        }
    }
}
