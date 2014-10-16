using Autofac;

namespace Specify.Containers
{
    public class AutofacDependencyResolver : IDependencyResolver
    {
        private IContainer _container;

        public AutofacDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public IDependencyScope CreateScope()
        {
            return new AutofacDependencyScope(_container.BeginLifetimeScope());
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}