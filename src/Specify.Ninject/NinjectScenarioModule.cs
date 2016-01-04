using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ninject.Modules;
using Specify.lib;

namespace Specify.Ninject
{
    public class NinjectScenarioModule : NinjectModule
    {
        private readonly IEnumerable<Assembly> _assemblies;

        public NinjectScenarioModule(Assembly assembly)
            : this(new[] { assembly })
        {
        }

        public NinjectScenarioModule(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        public override void Load()
        {
            var scenarios = AssemblyTypeResolver
                .GetAllTypesFromAppDomain()
                .Where(type => type.IsScenario() && !type.IsAbstract);

            foreach (var scenario in scenarios)
            {
                Bind(scenario).ToSelf();
            }
        }
    }
}