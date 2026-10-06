using System;
using System.Collections.Generic;
using System.Text;

using CRM.DAL.Modules.Projects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DAL.Modules.Projects.Configurations
{
    public class BuildingConfiguration
        : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.ToTable("Buildings");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.Property(x => x.Entrance)
                .HasMaxLength(50);

            builder.Property(x => x.Address)
                .HasMaxLength(300);

            // קוד בניין חייב להיות ייחודי בתוך אותו פרויקט
            builder.HasIndex(x => new { x.ProjectId, x.Code })
                .IsUnique();

            builder.HasOne(x => x.Project)
                .WithMany(x => x.Buildings)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.BuildingStatus)
                .WithMany(x => x.Buildings)
                .HasForeignKey(x => x.BuildingStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}
