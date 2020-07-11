using System.Linq;
using ApiTemplate.Api.Domain.Common;
using ApiTemplate.Api.Domain.Model.ToDos;

namespace ApiTemplate.Api.Application.Common.Interfaces
{
    // This is used by Query Handlers in CQRS
    public interface IQueryDb 
    {
        IQueryable<ToDoItem> ToDoItems { get; }

        // This is a generic alternative to above. Team should discuss preference.
        IQueryable<T> QueryFor<T>() where T : Entity;
    }
}