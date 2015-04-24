using Autofac;
using TestStack.Seleno.Configuration;

namespace ContosoUniversity.FunctionalTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Autofac.Features.Variance;

    using ContosoUniversity.DAL;
    using ContosoUniversity.FunctionalTests.Specifications;
    using ContosoUniversity.Models;

    using MediatR;

    public class FunctionalTestsAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var selenoHost = new SelenoHost();
            selenoHost.Run("ContosoUniversity", 12365, 
                c => c.WithRouteConfig(RouteConfig.RegisterRoutes));
            builder.RegisterInstance(selenoHost).AsSelf().SingleInstance();
            builder.RegisterType<BrowserHost>().AsSelf();

            DependenciesConfig.ConfigureDependencies(builder);
            //builder.RegisterGeneric(typeof(ViewStudentDetailsScenario))
            //    .AsSelf()
            //    .PropertiesAutowired()
            //    .InstancePerLifetimeScope();

            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(FunctionalTestsAutofacModule).Assembly).AsImplementedInterfaces();
            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });

            builder.RegisterType<StudentDetailsPage>().As(typeof(IPage<Student>));
        }
    }
}