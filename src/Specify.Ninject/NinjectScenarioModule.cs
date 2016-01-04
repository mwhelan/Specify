using System.Linq;
using Ninject.Modules;
using Specify.lib;

namespace Specify.Ninject
{
    public class NinjectScenarioModule : NinjectModule
    {
        public override void Load()
        {
            // TODO: Is this the best way to load scenarios?
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