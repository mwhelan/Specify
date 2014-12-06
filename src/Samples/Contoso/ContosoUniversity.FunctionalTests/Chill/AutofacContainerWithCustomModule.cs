using Autofac;
using ContosoUniversity;
using ContosoUniversity.FunctionalTests;
using TestStack.Seleno.Configuration;
using TestStack.Seleno.Configuration.ControlIdGenerators;

namespace Chill.Autofac
{
    /// <summary>
    /// You can define 'non mocking' with all the type registrations that you would otherwise in your application. 
    /// This can either be done in the App. This class also creates a single 'parent' container and then child containers
    /// per test. 
    /// </summary>
    internal class AutofacContainerWithCustomModule : AutofacChillContainer
    {
        private static ILifetimeScope staticContainer;
        private static object syncRoot = new object();
        public AutofacContainerWithCustomModule()
            : base(CreateContainer())
        {

        }
        /// <summary>
        /// This method creates the Autofac container and registers the custom type 
        /// </summary>
        /// <returns></returns>
        private static ILifetimeScope CreateContainer()
        {
            if (staticContainer == null)
            {
                lock (syncRoot)
                {
                    if (staticContainer == null)
                    {
                        var builder = new ContainerBuilder();
                        //builder.RegisterModule<CustomAutofacModule>();
                        builder.RegisterType<BrowserHost>().InstancePerLifetimeScope();
                        builder.RegisterInstance(new SelenoHost()).SingleInstance();
                        staticContainer = DependenciesConfig.RegisterDependencies(builder);

                        staticContainer.Resolve<SelenoHost>()
                            .Run("ContosoUniversity", 18765, c =>
                                c.WithRouteConfig(RouteConfig.RegisterRoutes)
                                    .UsingControlIdGenerator(new MvcControlIdGenerator())
                                    .WithEnvironmentVariable("FunctionalTests")
                            );
                    }
                }
            }
            return staticContainer.BeginLifetimeScope();
        }
    }
}
