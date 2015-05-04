using System.Linq;
using Specify.lib;
using Autofac;

namespace Specify.Autofac
{
    public class AutofacDependencyResolver : AutofacContainer, IDependencyResolver
    {
        public AutofacDependencyResolver()
        {
            var assemblies = AssemblyTypeResolver.GetAllAssembliesFromAppDomain().ToArray();
            _containerBuilder.RegisterAssemblyModules(assemblies);

        }
        public IScenarioContainer CreateChildContainer()
        {
            return new AutofacContainer(Container.BeginLifetimeScope());
        }
    }
}