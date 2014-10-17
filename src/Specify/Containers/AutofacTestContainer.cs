using Autofac;

namespace Specify.Containers
{
    public class AutofacTestContainer : ITestContainer
    {
        private IContainer _container;

        public AutofacTestContainer(IContainer container)
        {
            _container = container;
        }

        public IDependencyResolver CreateTestLifetimeScope()
        {
            return new AutofacDependencyResolver(_container.BeginLifetimeScope());
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}