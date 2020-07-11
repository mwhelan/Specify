using ApiTemplate.Api.Application.Common.Interfaces;
using ApiTemplate.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiTemplate.Api.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServicesForInfrastructureProject(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging();

            services.AddScoped<DbContextOptions<AppDbContext>>(serviceProvider =>
            {
                var connectionString = configuration.GetConnectionString("AppDb");
                return new DbContextOptionsBuilder<AppDbContext>()
                      .UseSqlServer(connectionString, options => options.EnableRetryOnFailure())
                      .Options;
            });
             
             services.AddScoped<AppDbContext>(serviceProvider =>
             {
                 var userService = serviceProvider.GetRequiredService<ICurrentUserService>(); // Get from parent service provider (ASP.NET MVC Pipeline)
                 var options = serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>();
                 return new AppDbContext(options, userService);
             });

            services.AddScoped<IUnitOfWork, AppDbContext>();
            services.AddScoped<IQueryDb, QueryDbContext>();

            return services;
        }
    }
}