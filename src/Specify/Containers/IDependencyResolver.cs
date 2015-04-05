using System.Linq;
using Autofac;
using Specify.lib;

namespace Specify.Containers
{
    public interface IDependencyResolver : IContainer
    {
        IContainer CreateChildContainer();
    }

    public class AutofacDependencyResolver : IocContainer, IDependencyResolver
    {
        public AutofacDependencyResolver()
        {
            var assemblies = AssemblyTypeResolver.GetAllAssembliesFromAppDomain().ToArray();
            _containerBuilder.RegisterAssemblyModules(assemblies);

        }
        public IContainer CreateChildContainer()
        {
            return new IocContainer(Container.BeginLifetimeScope());
        }
    }
}
