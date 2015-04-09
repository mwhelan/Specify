using System;
using System.Linq;
using Ninject;
using Ninject.Modules;
using Ninject.Extensions.ChildKernel;
using Specify.Containers;
using Specify.lib;

namespace Specify.Examples.Ninject
{
    public class NinjectDependencyResolver : NinjectContainer, IDependencyResolver
    {
        public const string ScenarioLifetimeScopeTag = "ScenarioLifetime";

        public NinjectDependencyResolver() 
            : base(CreateContainer()) { }

        private static IKernel CreateContainer()
        {
            var assemblies = AssemblyTypeResolver.GetAllAssembliesFromAppDomain().ToArray();
            //var modules = from assembly in assemblies
            //              from type in assembly.GetTypes()
            //              where type.CanBeCastTo<INinjectModule>()
            //              select (INinjectModule)Activator.CreateInstance(type);

            //var kernel = new StandardKernel();
            //kernel.Load(assemblies);
            //var ninjectModules = kernel.GetModules();

            var modules = AssemblyTypeResolver
                .GetAllTypesFromAppDomain()
                .Where(x => x.CanBeCastTo<INinjectModule>())
                .Distinct()
                .Select (type => (NinjectModule)Activator.CreateInstance(type, assemblies))
                .ToArray();
            var kernel = new StandardKernel(modules);
            return kernel;
        }

        public IContainer CreateChildContainer()
        {
            var childContainer = new ChildKernel(this.Container);
            return new NinjectContainer(childContainer);
        }
    }
}