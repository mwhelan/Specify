using ApiTemplate.Api.Domain.Model.ToDos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTemplate.Api.Infrastructure.Persistence.Configurations
{
    public class TodoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
    {
        public void Configure(EntityTypeBuilder<ToDoItem> builder)
        {
            builder.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.Description)
                .HasMaxLength(400)
                .IsRequired();

            // Use EF value conversions for single-property value objects
            builder.Property(t => t.Email)
                .HasConversion(p => p.Value, p => Email.Create(p).Value)
                .HasMaxLength(250);
        }
    }
}