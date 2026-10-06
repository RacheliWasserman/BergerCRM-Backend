using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CRM.DAL.Modules.Leads.Entities;

namespace CRM.DAL.Modules.Leads.Configurations
{
    public class LeadStatusHistoryConfiguration : IEntityTypeConfiguration<LeadStatusHistory>
    {
        public void Configure(EntityTypeBuilder<LeadStatusHistory> builder)
        {
            builder.ToTable("LeadStatusHistory");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ChangedAt)
                .IsRequired();

            builder.Property(x => x.Reason)
                .HasMaxLength(500);

            builder.Property(x => x.Notes)
                .HasMaxLength(1000);

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.HasIndex(x => x.LeadId);
            builder.HasIndex(x => x.ChangedAt);

            builder.HasOne(x => x.Lead)
                .WithMany(x => x.StatusHistory)
                .HasForeignKey(x => x.LeadId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.LeadStatus)
                .WithMany(x => x.LeadStatusHistories)
                .HasForeignKey(x => x.LeadStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ChangedByUser)
                .WithMany()
                .HasForeignKey(x => x.ChangedByUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
