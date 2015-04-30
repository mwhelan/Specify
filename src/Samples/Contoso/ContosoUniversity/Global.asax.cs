using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ContosoUniversity
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            DependenciesConfig.RegisterDependencies();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MappingConfig.ConfigureMappings();
        }
    }
}
