using System;
using Autofac;
using AutofacContrib.NSubstitute;

namespace Specify.Containers
{
    public class NSubstituteDependencyResolver : IDependencyResolver
    {
        private readonly AutoSubstitute _scope = new AutoSubstitute();

        public TService Resolve<TService>()
        {
            return _scope.Resolve<TService>();
        }

        public object Resolve(Type type)
        {
            return _scope.Container.Resolve(type);
        }

        public void Inject<TService>(TService instance) where TService : class
        {
            _scope.Provide<TService>(instance);
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}