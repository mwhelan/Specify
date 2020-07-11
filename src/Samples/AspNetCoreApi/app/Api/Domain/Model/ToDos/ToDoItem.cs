using ApiTemplate.Api.Domain.Common;

namespace ApiTemplate.Api.Domain.Model.ToDos
{
    public class ToDoItem : Entity
    {
        public ToDoItem(string title, string description, Email email)
        {
            Title = title;
            Description = description;
            Email = email;
        }

        public ToDoItem(int id, string title, string description, Email email) 
            : this(title, description, email)
        {
            Id = id;
        }

        public ToDoItem() { }

        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; }
        public Email Email { get; private set; }
        public bool IsDone { get; private set; }

        public void SetCompleted()
        {
            IsDone = true;
        }

        public void Update(string title, string description, Email email, bool isDone)
        {
            Title = title;
            Description = description;
            Email = email;
            IsDone = isDone;
        }
    }
}