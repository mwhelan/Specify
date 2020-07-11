using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;
using ApiTemplate.Api.Application.Common.Interfaces;
using ApiTemplate.Api.Common;
using ApiTemplate.Api.Configuration.Options;
using ApiTemplate.Api.Infrastructure.Identity;
using ApiTemplate.Api.Infrastructure.Logging;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace ApiTemplate.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServicesForApiProject(this IServiceCollection services,
            IConfiguration configuration, IHostEnvironment environment)
        {
            services.AddSecurity(configuration);
            services.AddControllers(options =>
                {
                    options.Filters.Add<SerilogLoggingActionFilter>();

                    if (!environment.IsTest() )
                    {
                        var policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
                        options.Filters.Add(new AuthorizeFilter(policy));
                    }
                })
                .AddControllersAsServices()
                .AddNewtonsoftJson();

            services.AddProblemDetails();

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHealthChecks()
                .AddSqlServer(connectionString: configuration["ConnectionStrings:AppDb"],
                    failureStatus: HealthStatus.Unhealthy, tags: new[] { "ready" });

            services.AddSwagger();

            return services;
        }

        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var identityServerOptions = new IdentityServerOptions();
            configuration.GetSection(nameof(IdentityServerOptions)).Bind(identityServerOptions);

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = identityServerOptions.IdentityServerUrl;
                    options.Audience = "apitemplate_api";
                    options.RequireHttpsMetadata = identityServerOptions.RequiresHttps;
                });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo {Title = "API Template", Version = "v1"});

                x.ExampleFilters();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);


            });

            services.AddSwaggerExamplesFromAssemblyOf<Startup>();
            
            return services;
        }
    }
}