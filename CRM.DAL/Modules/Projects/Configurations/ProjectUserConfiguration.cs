using System;
using System.Collections.Generic;
using System.Text;

using CRM.DAL.Modules.Projects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DAL.Modules.Projects.Configurations
{
    public class ProjectUserConfiguration : IEntityTypeConfiguration<ProjectUser>
    {
        public void Configure(EntityTypeBuilder<ProjectUser> builder)
        {
            builder.ToTable("ProjectUsers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProjectRole)
                .HasMaxLength(100);

            builder.Property(x => x.AssignedAt)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.HasOne(x => x.Project)
                .WithMany(x => x.ProjectUsers)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // אותו משתמש לא יופיע פעמיים באותו פרויקט
            builder.HasIndex(x => new
            {
                x.ProjectId,
                x.UserId
            }).IsUnique();
        }
    }
}
