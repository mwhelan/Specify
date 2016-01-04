using Ninject;
using Specify.Configuration;

namespace Specify.Ninject
{
    public class SpecifyNinjectBootstrapper : SpecifyConfiguration
    {
        protected override IContainer BuildApplicationContainer()
        {
            var mockFactory = GetMockFactory();
            var kernel = new NinjectContainerFactory().Create(mockFactory);
            ConfigureContainer(kernel);

            return new NinjectContainer(kernel);
        }

        public virtual void ConfigureContainer(IKernel kernel)
        {

        }
    }
}
