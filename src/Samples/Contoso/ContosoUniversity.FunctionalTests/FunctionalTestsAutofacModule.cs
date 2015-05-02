using Autofac;
using Autofac.Features.Variance;
using ContosoUniversity.FunctionalTests.Specifications;
using MediatR;
using System.Collections.Generic;
using ContosoUniversity.Domain.Model;
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
            builder.RegisterType<WebUiDriver>().AsSelf();

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