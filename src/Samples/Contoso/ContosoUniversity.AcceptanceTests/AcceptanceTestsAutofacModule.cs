using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Net.NetworkInformation;
using Autofac;
using Autofac.Features.Variance;
using ContosoUniversity.AcceptanceTests.Infrastructure;
using ContosoUniversity.DAL;
using MediatR;

namespace ContosoUniversity.AcceptanceTests
{
    public class AcceptanceTestsAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var testPath = Path.GetDirectoryName(GetType().Assembly.CodeBase.Replace("file:///", ""));
            AppDomain.CurrentDomain.SetData("DataDirectory", testPath);// For localdb connection string that uses |DataDirectory|        
           // Database.SetInitializer(new DropCreateDatabaseAlways<SchoolContext>());
            using (var context = new SchoolContext())
            {
                context.Database.Initialize(false);
                SeedData.Create(context);
            }
            
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
