using System.Collections.Generic;
using Autofac;
using Autofac.Features.Variance;
using ContosoUniversity.AcceptanceTests.Infrastructure;
using MediatR;

namespace ContosoUniversity.AcceptanceTests
{
    public class AcceptanceTestsAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            DependenciesConfig.ConfigureDependencies(builder);
            builder.RegisterGeneric(typeof (ViewControllerScenario<,>))
                .AsSelf()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(AcceptanceTestsAutofacModule).Assembly).AsImplementedInterfaces();
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

        }
    }
}
