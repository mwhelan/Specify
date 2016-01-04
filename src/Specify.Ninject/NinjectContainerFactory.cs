using System;
using Ninject;
using Ninject.Extensions.ChildKernel;
using Specify.Mocks;

namespace Specify.Ninject
{
    internal class NinjectContainerFactory
    {
        public IKernel Create(Func<IMockFactory> mockFactory)
        {
            var kernel = new StandardKernel();
            RegisterScenarios(kernel);
            RegisterScenarioContainer(kernel, mockFactory);

            return kernel;
        }

        private void RegisterScenarios(IKernel kernel)
        {
            // TODO: Register scenarios
            kernel.Load<NinjectScenarioModule>();
        }

        private void RegisterScenarioContainer(IKernel kernel, Func<IMockFactory> mockFactory)
        {
            // TODO: Register IScenarioContainer as full Ninject Ioc container or mocking container with provided mockFactory

            //var childContainer = new ChildKernel(kernel);
            //IContainer container = new NinjectContainer(childContainer);
        }
    }
}