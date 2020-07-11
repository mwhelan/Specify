using ApiTemplate.Api.Domain.Model.MasterFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTemplate.Api.Infrastructure.Persistence.Configurations
{
    public class WeatherConditionConfiguration : IEntityTypeConfiguration<WeatherCondition>
    {
        public void Configure(EntityTypeBuilder<WeatherCondition> builder)
        {
            builder.ToTable("sw_WeatherT");

            builder.Property(e => e.Id)
                .HasColumnName("WeatherID");

            builder.Property(t => t.Condition)
                .HasColumnName("WeatherCondition")
                .HasMaxLength(200)
                .IsRequired();

            builder
                .HasOne(p => p.WeatherType)
                .WithMany()
                .HasForeignKey("WeatherTypeID")
                .HasConstraintName("FK_sw_WeatherT_sw_Weather_TypeT");
        }
    }
}
