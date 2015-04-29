using System.Web.Routing;
using Autofac;
using ContosoUniversity.SubcutaneousTests.Infrastructure;
using Specify.Containers;

namespace ContosoUniversity.SubcutaneousTests
{
    public class SubcutaneousTestsAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            DependenciesConfig.ConfigureDependencies(builder);

            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);


            builder.Register(c => new MvcControllerDriver(routes) { Container = c.Resolve<Specify.Containers.IContainer>() })
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.Register(c => new IocContainer(c.Resolve<ILifetimeScope>()))
                .As<Specify.Containers.IContainer>()
                .InstancePerLifetimeScope();
        }
    }
}