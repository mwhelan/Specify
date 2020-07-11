using System;
using System.Linq;
using ApiTemplate.Api.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Api.Infrastructure.Persistence
{
    public class ShadowProperties
    {
        public const string CreatedByUserName = "CreatedByUserName";
        public const string CreatedUtcDateTime = "CreatedUTCDateTime";
        public const string TimeStamp = "TimeStamp";

        public static void ConfigureAuditForAllEntities(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(e => typeof(Entity).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entityType.Name)
                    .Property<string>(ShadowProperties.CreatedByUserName)
                    .HasMaxLength(400)
                    .IsUnicode(false);
                modelBuilder.Entity(entityType.Name)
                    .Property<DateTime>(ShadowProperties.CreatedUtcDateTime)
                    .HasColumnType("datetime")
                    .IsRequired();

                // need to configure concurrency before turning this on
                //modelBuilder.Entity(entityType.Name)
                //    .Property<byte[]>(ShadowProperties.TimeStamp)
                //    .IsRequired()
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            }
        }
    }
}