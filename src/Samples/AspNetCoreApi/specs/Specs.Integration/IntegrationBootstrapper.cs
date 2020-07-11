using ApiTemplate.Api.Application.Common.Interfaces;
using ApiTemplate.Api.Infrastructure.Persistence;
using Autofac;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Specify.Autofac;
using Specify.Configuration;
using Specify.Mocks;
using Specs.Library;
using Specs.Library.Builders.ValueSuppliers;
using Specs.Library.Data;
using Specs.Library.Data.SqlServer;
using Specs.Library.Drivers.Api;
using Specs.Library.Extensions;
using Specs.Library.Identity;
using TestStack.BDDfy.Configuration;
using TestStack.Dossier;
using IContainer = Specify.IContainer;

namespace Specs.Integration.ApiTemplate
{
    /// <summary>
    /// The startup class to configure Specify with the Autofac container. 
    /// Make any changes to the default configuration settings in this file.
    /// </summary>
    public class IntegrationBootstrapper : DefaultAutofacBootstrapper
    {
        private IHost _host;

        //IN MEMORY CONFIG
        public IntegrationBootstrapper()
        {
            HtmlReport.ReportHeader = "API Template";
            HtmlReport.ReportDescription = "Integration Specifications";
            HtmlReport.OutputFileName = "ApiTemplate-IntegrationSpecs.html";
            Configurator.BatchProcessors.HtmlReport.Disable();

            // Because TestStack.Dossier uses NSubstitute need to tell Specify not to mock
            MockFactory = new NullMockFactory();

            AnonymousValueFixture.GlobalValueSuppliers.Add(new CodeValueSupplier());
        }

        protected override IContainer BuildApplicationContainer()
        {
            _host = TestSettings
                .CreateHost(ConfigureContainerForTests,
                    services => services.SwapScoped<ICurrentUserService, TestCurrentUserService>());
            var container = _host.ToAutofacContainer();

            return new AutofacContainer(container);
        }

        private void ConfigureContainerForTests(ContainerBuilder builder)
        {
            ConfigureContainerForSpecify(builder);

            builder.RegisterType<AsyncApiDriver>().AsSelf();

            // Database
            builder.RegisterType<SqlDb>().As<IDb>().SingleInstance();
            builder.Register(c =>
            {
                var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlServer(TestSettings.ConnectionString)
                    .Options;
                return new SqlDbFactory(options);
            })
                .As<IDbFactory>()
                .SingleInstance();

            builder.RegisterType<ResetDatabaseAction>().As<IPerScenarioAction>();
            builder.RegisterType<ResetSystemTimeAction>().As<IPerScenarioAction>();
            builder.RegisterType<DatabaseApplicationAction>().As<IPerApplicationAction>();

            if (TestSettings.InMemoryMode)
            {
                ConfigureForInMemory(builder);
            }
            else
            {
                builder.RegisterType<WebHost>().As<ITestHost>().InstancePerLifetimeScope();
            }

        }

        private void ConfigureForInMemory(ContainerBuilder builder)
        {
            builder.RegisterType<MemoryHost>().As<ITestHost>().InstancePerLifetimeScope();
            builder.Register(ctx =>
            {
                return _host.GetTestServer();
            })
                .As<TestServer>()
                .SingleInstance();
        }
    }
}