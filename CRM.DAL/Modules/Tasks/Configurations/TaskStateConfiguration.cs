using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CRM.DAL.Modules.Tasks.Entities;

namespace CRM.DAL.Modules.Tasks.Configurations
{
    public class TaskStateConfiguration : IEntityTypeConfiguration<TaskState>
    {
        public void Configure(EntityTypeBuilder<TaskState> builder)
        {
            builder.ToTable("TaskStatuses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Code).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.HasIndex(x => x.Code).IsUnique();
        }
    }
}
