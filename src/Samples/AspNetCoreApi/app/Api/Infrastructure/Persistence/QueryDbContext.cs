using System.Linq;
using ApiTemplate.Api.Application.Common.Interfaces;
using ApiTemplate.Api.Domain.Common;
using ApiTemplate.Api.Domain.Model.ToDos;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Api.Infrastructure.Persistence
{
    public class QueryDbContext : IQueryDb
    {
        private readonly AppDbContext _db;

        public QueryDbContext(AppDbContext db)
        {
            _db = db;
            db.ChangeTracker.AutoDetectChangesEnabled = false; // is this needed with QueryTrackingBehavior?
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public IQueryable<ToDoItem> ToDoItems => _db.ToDoItems;

        // This is a generic alternative to above. Team should discuss preference.
        public IQueryable<T> QueryFor<T>() where T : Entity
        {
            return _db.Set<T>();
        }
    }
}