using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CRM.DAL.Modules.Leads.Entities;

namespace CRM.DAL.Modules.Leads.Configurations
{
    public class LeadPreferenceConfiguration : IEntityTypeConfiguration<LeadPreference>
    {
        public void Configure(EntityTypeBuilder<LeadPreference> builder)
        {
            builder.ToTable("LeadPreferences");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.MinBudget).HasPrecision(18, 2);
            builder.Property(x => x.MaxBudget).HasPrecision(18, 2);

            builder.Property(x => x.MinRooms).HasPrecision(4, 1);
            builder.Property(x => x.MaxRooms).HasPrecision(4, 1);

            builder.Property(x => x.MinArea).HasPrecision(18, 2);
            builder.Property(x => x.MaxArea).HasPrecision(18, 2);

            builder.Property(x => x.PreferredCity).HasMaxLength(100);

            builder.Property(x => x.PreferredDirections).HasMaxLength(300);
            builder.Property(x => x.Notes).HasMaxLength(1000);

            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.HasIndex(x => x.LeadId).IsUnique();

            builder.HasOne(x => x.Lead)
                .WithOne(x => x.Preference)
                .HasForeignKey<LeadPreference>(x => x.LeadId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
