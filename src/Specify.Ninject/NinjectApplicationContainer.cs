using System.Reflection;
using Ninject;
using Ninject.Extensions.ChildKernel;

namespace Specify.Ninject
{
    public class NinjectApplicationContainer : NinjectScenarioContainer, IApplicationContainer
    {
        public NinjectApplicationContainer()
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
            return new NinjectScenarioContainer(childContainer);
        }
    }
}