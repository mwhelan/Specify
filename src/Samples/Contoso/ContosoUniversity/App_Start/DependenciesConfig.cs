using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.DAL.Repositories;
using MicroLite;

namespace ContosoUniversity
{
    public static class DependenciesConfig
    {
        public static IContainer RegisterDependencies(ContainerBuilder builder = null)
        {
            if(builder == null) 
                builder = new ContainerBuilder();

            ConfigureDependencies(builder);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            return container;
        }

        public static void ConfigureDependencies(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //var sessionFactory = DatabaseConfig.BuildSessionFactory();
            //builder.RegisterInstance(sessionFactory).SingleInstance();
            //builder.Register(c => c.Resolve<ISessionFactory>().OpenSession())
            //    .As<ISession>()
            //    .InstancePerRequest();
            //builder.RegisterType<SchoolRepository>().As<ISchoolRepository>();//.InstancePerRequest();
            builder.RegisterType<SchoolContext>().AsSelf();//.InstancePerRequest();
            builder.RegisterType<EfSchoolRepository>().As<ISchoolRepository>();//.InstancePerRequest();
        }
    }
}