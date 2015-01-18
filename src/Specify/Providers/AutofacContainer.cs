using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Specify.Configuration;
using Module = Autofac.Module;

namespace Specify.Providers
{
    internal class AutofacContainer : ISpecifyContainer
    {
        private ILifetimeScope _container;

        public ISpecification Resolve(Type type)
        {
            return (ISpecification)Container.Resolve(type);
        }

        public virtual ITestLifetimeScope CreateTestLifetimeScope()
        {
            return new AutofacTestLifetimeScope(Container.BeginLifetimeScope());
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public ILifetimeScope Container
        {
            get
            {
                if (_container == null)
                {
                    var builder = new ContainerBuilder();
                    builder.RegisterAssemblyModules(GetType().Assembly, Assembly.GetExecutingAssembly());
                    _container = builder.Build();
                }

                return _container;
            }
        }
    }

    public class SpecifyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //var assemblies = AssemblyTypeResolver.GetAllAssembliesFromAppDomain();
            //builder.RegisterAssemblyTypes(assemblies.ToArray())
            //       .Where(t => typeof(ISpecification).IsAssignableFrom(t))
            //       .AsImplementedInterfaces();            
            
            //foreach (var specification in ScanForSpecificationTypes())
            //{
            //    builder.RegisterType(specification)
            //        //.PropertiesAutowired()
            //        .InstancePerLifetimeScope();
            //}
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