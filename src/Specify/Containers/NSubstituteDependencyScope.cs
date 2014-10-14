using AutofacContrib.NSubstitute;

namespace Specify.Containers
{
    public class NSubstituteDependencyScope : IDependencyScope
    {
        private readonly AutoSubstitute _scope = new AutoSubstitute();

        public TService Get<TService>()
        {
            return _scope.Resolve<TService>();
        }

        public void Set<TService>(TService instance) where TService : class
        {
            _scope.Provide<TService>(instance);
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}