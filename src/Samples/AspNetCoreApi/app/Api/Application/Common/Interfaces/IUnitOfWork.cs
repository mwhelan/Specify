using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using ApiTemplate.Api.Domain.Model.ToDos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ApiTemplate.Api.Application.Common.Interfaces
{
    // This is used by Command Handlers in CQRS
    public interface IUnitOfWork
    {
        DbSet<ToDoItem> ToDoItems { get; set; }
        DbSet<WeatherCondition> WeatherConditions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IExecutionStrategy CreateExecutionStrategy();
        Task BeginTransactionAsync(IsolationLevel level = IsolationLevel.ReadCommitted);
        Task CommitTransactionAsync();
        void RollbackTransaction();
    }
}