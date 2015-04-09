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
            var modules = from assembly in assemblies
                          from type in assembly.GetTypes()
                          where type.CanBeCastTo<INinjectModule>()
                          select (INinjectModule)Activator.CreateInstance(type);

            return new StandardKernel(modules.ToArray());
        }

        public IContainer CreateChildContainer()
        {
            var childContainer = new ChildKernel(this.Container);
            return new NinjectContainer(childContainer);
        }
    }
}