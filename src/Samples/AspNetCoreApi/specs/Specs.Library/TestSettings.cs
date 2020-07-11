using System;
using System.Diagnostics;
using ApiTemplate.Api;
using ApiTemplate.Api.Configuration;
using ApiTemplate.Api.Infrastructure.Logging;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Specs.Library
{
    public static class TestSettings
    {
        private static IConfigurationRoot _configuration;
        public static IConfigurationRoot Configuration => _configuration ??= GetConfiguration();

        public static bool InMemoryMode => Configuration["TestExecutionMode"] == "InProcess";
        public static bool IsDebugMode
        {
            get
            {
                var isDebug = false;

#if (DEBUG)
                isDebug = true;
#else
                                isDebug = false;
#endif

                return isDebug;
            }
        }
        public static string ConnectionString => Configuration.GetConnectionString("AppDb");
        public static string EnvironmentName => Environment.GetEnvironmentVariable("NCrunch") == "1" ? "ncrunch" : "Test";
        public static bool ShouldCreateDatabase => IsDebugMode || EnvironmentName == "ncrunch";

        public static IHost CreateHost(
            Action<ContainerBuilder> containerOverrides = null,
            Action<IServiceCollection> servicesOverrides = null)
        {
            var serviceProviderFactory = new AppServiceProviderFactory(containerOverrides, servicesOverrides);

            Log.Logger = LoggingHelper.CreateLogger();

            var hostBuilder = new HostBuilder()
                .UseSerilog((hostingContext, loggerConfiguration) => {
                    loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .Enrich.FromLogContext()
                        .Enrich.WithProperty("ApplicationName", "ApiTemplate")
                        .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment);

#if DEBUG
                    // Used to filter out potentially bad data due to debugging.
                    // Very useful when doing Seq dashboards and want to remove logs under debugging session.
                    loggerConfiguration.Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached);
#endif
                })
                .UseServiceProviderFactory(serviceProviderFactory)
                .ConfigureWebHostDefaults(webHost =>
                {
                    webHost
                        .ConfigureLogging(builder => builder.AddSeq())
                        .UseTestServer()
                        .UseConfiguration(Configuration)
                        .UseEnvironment(EnvironmentName)
                        .UseStartup<Startup>();
                });
            return hostBuilder.Start();
        }

        public static ILifetimeScope ToAutofacContainer(this IHost webHost)
        {
            return webHost.Services.GetService<IServiceProvider>().GetAutofacRoot();
        }
        private static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder();

            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.AddJsonFile($"appsettings.{EnvironmentName}.json", optional: false, reloadOnChange: true);
            builder.AddEnvironmentVariables();

            var config = builder.Build();

            return config;
        }
    }
}