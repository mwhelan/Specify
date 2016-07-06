using Autofac;
using Specify.Configuration;

namespace Specify.Autofac
{
    public class DefaultAutofacBootstrapper : BootstrapperBase
    {
        protected override IContainer BuildApplicationContainer()
        {
            var mockFactory = GetMockFactory();
            var builder = new AutofacContainerFactory().Create(mockFactory);
            ConfigureContainer(builder);
            var container = builder.Build();
            return new AutofacContainer(container);
        }

        public virtual void ConfigureContainer(ContainerBuilder builder)
        {

        }
    }
}