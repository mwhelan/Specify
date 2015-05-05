using System.Reflection;
using Ninject;
using Ninject.Extensions.NamedScope;

namespace Specify.Ninject
{
    public class NinjectDependencyResolver : NinjectContainer, IDependencyResolver
    {
        public const string ScenarioLifetimeScopeTag = "ScenarioLifetime";

        public NinjectDependencyResolver(IKernel kernel) 
            : base(kernel) { }

        public NinjectDependencyResolver() 
            : base(CreateContainer()) { }

        private static IKernel CreateContainer()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }

        public IScenarioContainer CreateChildContainer()
        {
            Container.CreateNamedScope(ScenarioLifetimeScopeTag);
            return this;
        }
    }
}