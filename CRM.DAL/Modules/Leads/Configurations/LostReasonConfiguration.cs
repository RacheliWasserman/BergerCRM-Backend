using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CRM.DAL.Modules.Leads.Entities;

namespace CRM.DAL.Modules.Leads.Configurations
{
    public class LostReasonConfiguration : IEntityTypeConfiguration<LostReason>
    {
        public void Configure(EntityTypeBuilder<LostReason> builder)
        {
            builder.ToTable("LostReasons");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.HasIndex(x => x.Code).IsUnique();
        }
    }
}
