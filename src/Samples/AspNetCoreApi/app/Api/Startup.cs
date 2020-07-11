using ApiTemplate.Api.Application;
using ApiTemplate.Api.Infrastructure;
using ApiTemplate.Api.Infrastructure.HealthChecks;
using ApiTemplate.Api.Infrastructure.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Hellang.Middleware.ProblemDetails;

// See definitions of the DefaultApiConventions this applies to each HTTP verb here
//https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.defaultapiconventions?view=aspnetcore-3.1
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace ApiTemplate.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServicesForApiProject(_configuration, _environment);
            services.AddServicesForApplicationProject();
            services.AddServicesForInfrastructureProject(_configuration);
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseProblemDetails();

            app.UseSerilogRequestLogging(opts
                => opts.EnrichDiagnosticContext = LoggingHelper.EnrichFromRequest);

            //var swaggerOptions = new SwaggerOptions();
            //_configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            //app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });

            //app.UseSwaggerUI(option =>
            //{
            //    option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
            //});

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Template");
            });

          //  app.UseHttpsRedirection();

            app.UseSerilog();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //Override the ClientWebSiteHostUrl as an environment variable.
            app.UseCors(builder => builder.WithOrigins(_configuration.GetValue<string>("ClientWebSiteHostUrl"))
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
               );



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.ConfigureHealthChecks();
            });
        }
    }
}