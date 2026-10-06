using System;
using System.Collections.Generic;
using System.Text;

using CRM.DAL.Modules.Projects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DAL.Modules.Projects.Configurations
{
    public class ProjectAssetConfiguration
        : IEntityTypeConfiguration<ProjectAsset>
    {
        public void Configure(EntityTypeBuilder<ProjectAsset> builder)
        {
            builder.ToTable("ProjectAssets");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Name)
                .HasMaxLength(150);

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.Property(x => x.BuildingCode)
                .HasMaxLength(50);

            builder.Property(x => x.LocationDescription)
                .HasMaxLength(500);

            builder.Property(x => x.Area)
                .HasPrecision(18, 2);

            builder.Property(x => x.CurrentPrice)
                .HasPrecision(18, 2);

            // הקוד של החניה/מחסן חייב להיות ייחודי בתוך אותו פרויקט
            builder.HasIndex(x => new
            {
                x.ProjectId,
                x.Code
            }).IsUnique();

            builder.HasOne(x => x.Project)
                .WithMany(x => x.ProjectAssets)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AssetType)
                .WithMany(x => x.ProjectAssets)
                .HasForeignKey(x => x.AssetTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AssetStatus)
                .WithMany(x => x.ProjectAssets)
                .HasForeignKey(x => x.AssetStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}
