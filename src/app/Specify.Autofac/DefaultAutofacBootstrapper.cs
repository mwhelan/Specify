using Autofac;
using Specify.Configuration;

namespace Specify.Autofac
{
    public class DefaultAutofacBootstrapper : BootstrapperBase
    {
        protected override IContainerRoot BuildApplicationContainer()
        {
            var builder = ConfigureContainerForApplication();
            ConfigureContainerForSpecify(builder);
            var container = builder.Build();
            return new AutofacContainer(container);
        }

        public virtual ContainerBuilder ConfigureContainerForApplication()
        {
            return new ContainerBuilder();
        }

        public void ConfigureContainerForSpecify(ContainerBuilder builder)
        {
            builder.RegisterSpecify(MockFactory);
        }
    }
}