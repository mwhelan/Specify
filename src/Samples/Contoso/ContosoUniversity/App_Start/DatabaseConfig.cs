using System;
using System.Configuration;
using MicroLite;
using MicroLite.Configuration;
using MicroLite.Mapping;

namespace ContosoUniversity
{
    public static class DatabaseConfig
    {
        public static ISessionFactory BuildSessionFactory()
        {
            Configure.Extensions()
                .WithConventionBasedMapping(
                    new ConventionMappingSettings
                    {
                        UsePluralClassNameForTableName = false
                    });

            var connectionString = GetConnectionString();
            var sessionFactory = Configure
                .Fluently()
                .ForMsSql2012Connection(connectionString)
                .CreateSessionFactory();

            return sessionFactory;
        }

        private static string GetConnectionString()
        {
            var environmentVariable = Environment.GetEnvironmentVariable("FunctionalTests");
            var connectionString = environmentVariable == null
                ? "SchoolContext"
                : "SchoolContext-FunctionalTests";
            return connectionString;
        }
    }
}