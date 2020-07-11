using ApiTemplate.Api.Domain.Model.ToDos;
using TestStack.Dossier;
using TestStack.Dossier.Lists;

namespace Specs.Library.Builders.Entities
{
    public class ToDoItemBuilder : TestDataBuilder<ToDoItem, ToDoItemBuilder>
    {
        public ToDoItemBuilder()
        {
            Set(x => x.Email, Email.Create(Any.Person.EmailAddress()).Value);
        }

        public static ListBuilder<ToDoItem, ToDoItemBuilder> CreateDefaultList(int size = 3)
        {
            return ToDoItemBuilder.CreateListOfSize(size)
                .All()
                .Set(x => x.Email, () => Email.Create(Builders.Get.Any.Person.EmailAddress()).Value)
                .ListBuilder;
        }
    }
}