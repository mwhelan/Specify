using System;
using System.Diagnostics;
using ApiTemplate.Api.Configuration;
using ApiTemplate.Api.Infrastructure.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ApiTemplate.Api
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = LoggingHelper.CreateLogger();

            try
            {
                Log.Information("Starting host");

                var host = CreateHostBuilder(args).Build();

                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
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
                .UseServiceProviderFactory(new AppServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .CaptureStartupErrors(true);
                });
        }
    }
}