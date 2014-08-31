using Autofac;

namespace Specify.Containers
{
    public class AutofacResolver : AutofacDependencyLifetime
    {
        readonly IContainer _container;

        public AutofacResolver(IContainer container)
            : base(container)
        {
            _container = container;
        }

        public IDependencyLifetime BeginScope()
        {
            return new AutofacDependencyLifetime(_container.BeginLifetimeScope());
        }
    }
}