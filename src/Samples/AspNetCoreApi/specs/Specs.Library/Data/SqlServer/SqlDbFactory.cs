using ApiTemplate.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Respawn;
using Specs.Library.Identity;

namespace Specs.Library.Data.SqlServer
{
    /// <summary>
    /// SQL Server database provider
    /// </summary>
    public class SqlDbFactory : IDbFactory
    {
        protected readonly DbContextOptions<AppDbContext> DbOptions;
        private Checkpoint _checkpoint;

        public SqlDbFactory(DbContextOptions<AppDbContext> dbContextOptions)
        {
            DbOptions = dbContextOptions;

            _checkpoint = new Checkpoint
            {
                DbAdapter = DbAdapter.SqlServer,
                // ReSpawn will delete data from every table not in this white list
                TablesToIgnore = new[] {
                    "SchemaVersions",        // the DbUp migration history table
                    "sw_OrganisationT",
                    "sw_CompanyT",
                    "sw_UserT",
                    "sw_SiteT",
                    "sw_Produce_TypeT",
                    "sw_Certificate_TypeT",
                    "sys_Chemical_Unit_Of_MeasureT",
                    "sys_Spray_Instruction_StatusT",
                    "sys_Pay_TypeT",
                    "sw_BlockT",
                    "sw_VarietyT",
                    "sw_Block_VarietyT",
                    "sw_Block_Variety_ConfigurationT"
                }
            };
        }

        public AppDbContext CreateContext(bool beginTransaction = false)
        {
            var context = new AppDbContext(DbOptions, new TestCurrentUserService());
            if (beginTransaction)
                context.Database.BeginTransaction();
            return context;
        }

        public virtual void CreateDatabase()
        {
            using (var context = CreateContext(beginTransaction: false))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            };
        }

        public void DeleteDatabase()
        {
            using (var context = CreateContext(beginTransaction: false))
            {
                context.Database.EnsureDeleted();
            }
        }

        public void ResetData()
        {
            using (var connection = CreateContext(beginTransaction: false).Database.GetDbConnection())
            {
                connection.Open();
                _checkpoint.Reset(connection).Wait();
            }
        }
    }
}