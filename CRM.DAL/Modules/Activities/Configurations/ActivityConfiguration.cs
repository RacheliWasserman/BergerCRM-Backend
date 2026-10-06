using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CRM.DAL.Modules.Activities.Entities;

namespace CRM.DAL.Modules.Activities.Configurations
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Activities");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Subject).HasMaxLength(300);
            builder.Property(x => x.Description).HasMaxLength(4000);

            builder.Property(x => x.ActivityAt).IsRequired();

            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.HasIndex(x => x.ActivityTypeId);
            builder.HasIndex(x => x.ContactId);
            builder.HasIndex(x => x.CustomerId);
            builder.HasIndex(x => x.LeadId);
            builder.HasIndex(x => x.ProjectId);
            builder.HasIndex(x => x.UnitId);
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.ActivityAt);

            builder.HasOne(x => x.ActivityType)
                .WithMany(x => x.Activities)
                .HasForeignKey(x => x.ActivityTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Contact)
                .WithMany()
                .HasForeignKey(x => x.ContactId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Lead)
                .WithMany()
                .HasForeignKey(x => x.LeadId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Project)
                .WithMany()
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Unit)
                .WithMany()
                .HasForeignKey(x => x.UnitId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
