using Autofac;

namespace Specify.Containers
{
    public class AutofacResolver : AutofacDependencyLifetime
    {
        readonly IContainer container;

        public AutofacResolver(IContainer container)
            : base(container)
        {
            this.container = container;
        }

        public IDependencyLifetime BeginScope()
        {
            return new AutofacDependencyLifetime(container.BeginLifetimeScope());
        }
    }
}