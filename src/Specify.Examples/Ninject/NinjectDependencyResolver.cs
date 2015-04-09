using Ninject;
using Ninject.Extensions.ChildKernel;
using Specify.Containers;
using System.Reflection;

namespace Specify.Examples.Ninject
{
    public class NinjectDependencyResolver : NinjectContainer, IDependencyResolver
    {
        public const string ScenarioLifetimeScopeTag = "ScenarioLifetime";

        public NinjectDependencyResolver() 
            : base(CreateContainer()) { }

        private static IKernel CreateContainer()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }

        public IContainer CreateChildContainer()
        {
            var childContainer = new ChildKernel(this.Container);
            return new NinjectContainer(childContainer);
        }
    }
}