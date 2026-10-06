using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CRM.DAL.Modules.Leads.Entities;

namespace CRM.DAL.Modules.Leads.Configurations
{
    public class LeadInterestedUnitConfiguration : IEntityTypeConfiguration<LeadInterestedUnit>
    {
        public void Configure(EntityTypeBuilder<LeadInterestedUnit> builder)
        {
            builder.ToTable("LeadInterestedUnits");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AddedAt).IsRequired();

            builder.Property(x => x.Notes).HasMaxLength(1000);

            builder.Property(x => x.IsActive).HasDefaultValue(true);

            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.HasIndex(x => new { x.LeadId, x.UnitId }).IsUnique();
            builder.HasIndex(x => x.LeadId);
            builder.HasIndex(x => x.UnitId);

            builder.HasOne(x => x.Lead)
                .WithMany(x => x.InterestedUnits)
                .HasForeignKey(x => x.LeadId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Unit)
                .WithMany()
                .HasForeignKey(x => x.UnitId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AddedByUser)
                .WithMany()
                .HasForeignKey(x => x.AddedByUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
