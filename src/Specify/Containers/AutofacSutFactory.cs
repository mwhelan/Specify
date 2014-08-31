using System;
using Autofac;
using Autofac.Builder;

namespace Specify.Containers
{
    public class AutofacSutFactory<TSut> : ISutResolver<TSut> where TSut : class
    {
        private ILifetimeScope _scope;
        private TSut _systemUnderTest;

        public AutofacSutFactory(ILifetimeScope scope)
        {
            if (scope == null)
                throw new ArgumentNullException("scope");

            _scope = scope;
        }
        public TService Resolve<TService>()
        {
            if (_scope == null)
                throw new ObjectDisposedException("this", "This scope has already been disposed.");

            return _scope.Resolve<TService>();
        }

        public TSut SystemUnderTest()
        {
            if (_systemUnderTest == null)
            {
                _systemUnderTest = _scope.Resolve<TSut>();
            }
            return _systemUnderTest;
        }

        public void Inject<TService>(TService instance) where TService : class
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