using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Specify
{
    public class LightInjectDependencyResolver : LightInjectContainer, IDependencyResolver
    {
        public LightInjectDependencyResolver()
        {
            //var assemblies = AssemblyTypeResolver.GetAllAssembliesFromAppDomain().ToArray();
            //_containerBuilder.RegisterAssemblyModules(assemblies);

        }
        public IScenarioContainer CreateChildContainer()
        {
            Container.BeginScope();
            return new LightInjectContainer(Container);
        }

    }
}
