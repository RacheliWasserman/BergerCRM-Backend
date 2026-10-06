using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CRM.DAL.Modules.Tasks.Entities;

namespace CRM.DAL.Modules.Tasks.Configurations
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Description).HasMaxLength(4000);

            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.HasIndex(x => x.TaskStateId);
            builder.HasIndex(x => x.PriorityId);
            builder.HasIndex(x => x.AssignedToUserId);
            builder.HasIndex(x => x.CreatedForLeadId);
            builder.HasIndex(x => x.CustomerId);
            builder.HasIndex(x => x.ProjectId);
            builder.HasIndex(x => x.DueAt);
            builder.HasIndex(x => x.IsActive);

            builder.HasOne(x => x.TaskState)
                .WithMany(x => x.TaskItems)
                .HasForeignKey(x => x.TaskStateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Priority)
                .WithMany(x => x.TaskItems)
                .HasForeignKey(x => x.PriorityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AssignedToUser)
                .WithMany()
                .HasForeignKey(x => x.AssignedToUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.CreatedForLead)
                .WithMany()
                .HasForeignKey(x => x.CreatedForLeadId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Project)
                .WithMany()
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
