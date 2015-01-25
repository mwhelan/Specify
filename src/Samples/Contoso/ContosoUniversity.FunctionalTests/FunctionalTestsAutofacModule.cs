using Autofac;
using TestStack.Seleno.Configuration;

namespace ContosoUniversity.FunctionalTests
{
    public class FunctionalTestsAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var selenoHost = new SelenoHost();
            selenoHost.Run("ContosoUniversity", 12365, 
                c => c.WithRouteConfig(RouteConfig.RegisterRoutes));
            builder.RegisterInstance(selenoHost).AsSelf().SingleInstance();
            builder.RegisterType<BrowserHost>().AsSelf();
        }
    }
}