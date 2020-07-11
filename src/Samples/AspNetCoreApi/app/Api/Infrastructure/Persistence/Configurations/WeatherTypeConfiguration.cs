using ApiTemplate.Api.Domain.Model.MasterFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTemplate.Api.Infrastructure.Persistence.Configurations
{
    public class WeatherTypeConfiguration : IEntityTypeConfiguration<WeatherType>
    {
        public void Configure(EntityTypeBuilder<WeatherType> builder)
        {
            builder.ToTable("sw_Weather_TypeT");

            builder.Property(e => e.Id)
                .HasColumnName("WeatherTypeID");

            builder.Property(t => t.WeatherTypeName)
                .HasColumnName("WeatherType")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.RequiredFlag)
                .HasDefaultValue(false)
                .IsRequired();
        }
    }
}
