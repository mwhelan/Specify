using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.DAL.Repositories;

namespace ContosoUniversity
{
    public static class DependenciesConfig
    {
        public static IContainer RegisterDependencies(ContainerBuilder builder = null)
        {
            if(builder == null) 
                builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<SchoolContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<SchoolRepository>().As<ISchoolRepository>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            return container;
        }
    }
}