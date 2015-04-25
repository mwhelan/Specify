using System.Web.Routing;
using Autofac;
using ContosoUniversity.SubcutaneousTests.Infrastructure;

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
        }
    }
}