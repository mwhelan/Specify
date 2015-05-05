using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ninject.Modules;

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
            var scenarios = from assembly in _assemblies 
                            from type in assembly.GetTypes() 
                            where type.IsScenario() 
                            select type;

            foreach (var scenario in scenarios)
            {
                Bind(scenario).ToSelf();
            }
        }
    }
}