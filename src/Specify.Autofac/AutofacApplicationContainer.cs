using System.Linq;
using Specify.lib;
using Autofac;

namespace Specify.Autofac
{
    public class AutofacApplicationContainer : AutofacScenarioContainer, IApplicationContainer
    {
        public AutofacApplicationContainer()
        {
            var assemblies = AssemblyTypeResolver.GetAllAssembliesFromAppDomain().ToArray();
            _containerBuilder.RegisterAssemblyModules(assemblies);

        }
        public IScenarioContainer CreateChildContainer()
        {
            return new AutofacScenarioContainer(Container.BeginLifetimeScope());
        }
    }
}