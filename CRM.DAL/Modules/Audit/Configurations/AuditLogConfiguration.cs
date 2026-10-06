using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CRM.DAL.Modules.Audit.Entities;

namespace CRM.DAL.Modules.Audit.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Action).IsRequired().HasMaxLength(100);
            builder.Property(x => x.EntityName).IsRequired().HasMaxLength(150);
            builder.Property(x => x.EntityId).HasMaxLength(100);

            builder.Property(x => x.IpAddress).HasMaxLength(100);

            // large text
            builder.Property(x => x.OldValues).HasColumnType("nvarchar(max)");
            builder.Property(x => x.NewValues).HasColumnType("nvarchar(max)");

            builder.Property(x => x.OccurredAt).IsRequired();

            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.EntityName);
            builder.HasIndex(x => x.OccurredAt);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
