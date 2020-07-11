using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Common.Interfaces;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using ApiTemplate.Api.Domain.Model.ToDos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace ApiTemplate.Api.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IUnitOfWork
    {
        private readonly ICurrentUserService _currentUserService;

        public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }
        
        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<DisposalReason> DisposalReasons { get; set; }
        public DbSet<WeatherType> WeatherTypes { get; set; }
        public DbSet<WeatherCondition> WeatherConditions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                // Just log SQL statements
                builder
                    .AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
                    .AddConsole();
            });

            optionsBuilder
                .UseLazyLoadingProxies()
                //.UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging();      // should be used in dev environment only
        }


        #region Transactions

        private IDbContextTransaction _currentTransaction;

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return Database.CreateExecutionStrategy();
        }

        public async Task BeginTransactionAsync(IsolationLevel level = IsolationLevel.ReadCommitted)
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = await Database.BeginTransactionAsync(level);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();

                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        #endregion
    }
}