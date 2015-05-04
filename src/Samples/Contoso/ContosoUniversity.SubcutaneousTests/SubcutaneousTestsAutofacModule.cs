using System.Web.Routing;
using Autofac;
using ContosoUniversity.SubcutaneousTests.Infrastructure;
using Specify;
using Specify.Autofac;

namespace ContosoUniversity.SubcutaneousTests
{
    public class SubcutaneousTestsAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            DependenciesConfig.ConfigureDependencies(builder);

            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);


            builder.Register(c => new MvcControllerDriver(routes) { Container = c.Resolve<IScenarioContainer>() })
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.Register(c => new AutofacContainer(c.Resolve<ILifetimeScope>()))
                .As<IScenarioContainer>()
                .InstancePerLifetimeScope();
        }
    }
}