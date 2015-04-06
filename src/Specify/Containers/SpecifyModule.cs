using System.Linq;
using Autofac;
using Specify.Configuration.Mocking;
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

            //builder.RegisterGeneric(typeof (ScenarioFor<>)).InstancePerLifetimeScope();
            //builder.RegisterGeneric(typeof (ScenarioFor<,>)).InstancePerLifetimeScope();

            var mockFactory = new MockDetector().FindAvailableMock() ;
            if (mockFactory == null)
            {
                builder.Register(c => new IocContainer(c.Resolve<ILifetimeScope>()))
                    .As<IContainer>()
                    .InstancePerLifetimeScope();
            }
            else
            {
                builder.Register(c => new AutoMockingContainer(mockFactory()))
                    .As<IContainer>()
                    .InstancePerLifetimeScope();
            }

        }
    }
}