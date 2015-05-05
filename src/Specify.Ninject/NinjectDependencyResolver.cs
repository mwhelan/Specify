using System.Reflection;
using Ninject;
using Ninject.Extensions.ChildKernel;

namespace Specify.Ninject
{
    public class NinjectDependencyResolver : NinjectContainer, IDependencyResolver
    {
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
            var childContainer = new ChildKernel(this.Container);
            return new NinjectContainer(childContainer);
        }
    }
}