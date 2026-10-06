using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CRM.DAL.Modules.Leads.Entities;

namespace CRM.DAL.Modules.Leads.Configurations
{
    public class LeadConfiguration : IEntityTypeConfiguration<Lead>
    {
        public void Configure(EntityTypeBuilder<Lead> builder)
        {
            builder.ToTable("Leads");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.LeadNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(x => x.LeadNumber).IsUnique();

            builder.Property(x => x.EstimatedBudget)
                .HasPrecision(18, 2);

            builder.Property(x => x.Notes)
                .HasMaxLength(2000);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.HasIndex(x => x.ContactId);
            builder.HasIndex(x => x.ProjectId);
            builder.HasIndex(x => x.LeadStatusId);
            builder.HasIndex(x => x.LeadSourceId);
            builder.HasIndex(x => x.AssignedUserId);
            builder.HasIndex(x => x.ReceivedAt);
            builder.HasIndex(x => x.IsActive);

            builder.HasOne(x => x.Contact)
                .WithMany()
                .HasForeignKey(x => x.ContactId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Project)
                .WithMany()
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.LeadSource)
                .WithMany(x => x.Leads)
                .HasForeignKey(x => x.LeadSourceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.LeadStatus)
                .WithMany(x => x.Leads)
                .HasForeignKey(x => x.LeadStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.LostReason)
                .WithMany(x => x.Leads)
                .HasForeignKey(x => x.LostReasonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AssignedUser)
                .WithMany()
                .HasForeignKey(x => x.AssignedUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
