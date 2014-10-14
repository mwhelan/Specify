using System;
using Autofac;
using Autofac.Builder;

namespace Specify.Containers
{
    public class AutofacDependencyScope : IDependencyScope
    {
        private ILifetimeScope _scope;

        public AutofacDependencyScope(ILifetimeScope scope)
        {
            if (scope == null)
                throw new ArgumentNullException("scope");

            _scope = scope;
        }
        public TService Get<TService>()
        {
            if (_scope == null)
                throw new ObjectDisposedException("this", "This scope has already been disposed.");

            return _scope.Resolve<TService>();
        }

        public object Resolve(Type type)
        {
            return _scope.Resolve(type);
        }

        public void Set<TService>(TService instance) where TService : class
        {
            _scope.ComponentRegistry.Register(RegistrationBuilder.ForDelegate((c, p) => instance)
                .InstancePerLifetimeScope().CreateRegistration());
        }

        public void Dispose()
        {
            _scope.Dispose();
            _scope = null;
        }
    }
}