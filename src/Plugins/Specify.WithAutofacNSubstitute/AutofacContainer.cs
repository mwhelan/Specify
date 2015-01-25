using System;

using Autofac;

namespace Specify.WithAutofacNSubstitute
{
    public class AutofacContainer : ISpecifyContainer
    {
        public ISpecification Resolve(Type type)
        {
            return (ISpecification)this.Container.Resolve(type);
        }

        public virtual ITestLifetimeScope CreateTestLifetimeScope()
        {
            return new AutofacTestLifetimeScope(this.Container.BeginLifetimeScope());
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        private ILifetimeScope _container;
        public ILifetimeScope Container
        {
            get
            {
                if (this._container == null)
                {
                    var builder = new ContainerBuilder();
                    builder.RegisterAssemblyModules(AppDomain.CurrentDomain.GetAssemblies());
                    _container = builder.Build();
                }

                return _container;
            }
        }
    }
}