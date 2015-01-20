using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;
using Specify.Configuration;

namespace Specify.WithAutofacNSubstitute
{
    public class SpecifyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = AssemblyTypeResolver.GetAllAssembliesFromAppDomain().ToArray();
            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(SpecificationFor<>));
            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(ScenarioFor<>));
        }

        public virtual IEnumerable<Type> ScanForSpecificationTypes()
        {
            return AssemblyTypeResolver
                .GetAllTypesFromAppDomain()
                .Where(t => typeof(ISpecification).IsAssignableFrom(t));
        }

    }
}