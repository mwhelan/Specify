using System.Linq;
using Autofac;
using Specify.lib;

namespace Specify.Containers
{
    public class SpecifyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = AssemblyTypeResolver.GetAllAssembliesFromAppDomain().ToArray();
            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(ScenarioFor<>));
            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(ScenarioFor<,>));
        }
    }
}