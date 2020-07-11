using ApiTemplate.Api.Domain.Model.MasterFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTemplate.Api.Infrastructure.Persistence.Configurations
{
    public class DisposalReasonConfiguration : IEntityTypeConfiguration<DisposalReason>
    {
        public void Configure(EntityTypeBuilder<DisposalReason> builder)
        {
            builder.ToTable("sw_Disposal_ReasonT")
                .HasIndex(e => e.Reason)
                .IsUnique();

            builder.Property(e => e.Id)
                .HasColumnName("DisposalReasonID");

            builder.Property(e => e.ActiveFlag)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(t => t.Reason)
                .HasColumnName("DisposalReason")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.DisposalReasonDescription)
                .HasMaxLength(500)
                .IsUnicode(false);
        }
    }
}