using Autofac;
using Specify.Configuration;
using Specify.Containers;
using TestStack.Seleno.Configuration;
using TestStack.Seleno.Configuration.ControlIdGenerators;

namespace ContosoUniversity.FunctionalTests
{
    public class SpecifyConfig : SpecifyConfiguration
    {
        private IDependencyResolver _dependencyResolver;

        public SpecifyConfig()
        {
            HtmlReport.Name = "ContosoAcceptanceTests.html";
            HtmlReport.Header = "Contoso University";
            HtmlReport.Description = "Acceptance Tests";
        }

        public override IDependencyResolver DependencyResolver()
        {
            if (_dependencyResolver == null)
            {
                var builder = new ContainerBuilder();
                builder.RegisterType<BrowserHost>().InstancePerLifetimeScope();
                builder.RegisterInstance(new SelenoHost()).SingleInstance();
                var container = DependenciesConfig.RegisterDependencies(builder);
                _dependencyResolver = new AutofacDependencyResolver(container);
            }
            return _dependencyResolver;
        }

        public override void BeforeAllTests()
        {
            _dependencyResolver
                .Resolve<SelenoHost>()
                .Run("ContosoUniversity", 18765, c =>
                    c.WithRouteConfig(RouteConfig.RegisterRoutes)
                    .UsingControlIdGenerator(new MvcControlIdGenerator())
                );
        }
    }

    public class BrowserHost
    {
        public SelenoHost Host { get; private set; }

        public BrowserHost(SelenoHost host)
        {
            Host = host;
        }
    }
}
