using System;
using System.Collections.Generic;
using System.Text;
using CRM.DAL.Modules.Projects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DAL.Modules.Projects.Configurations
{
    public class UnitAssetAssignmentConfiguration
        : IEntityTypeConfiguration<UnitAssetAssignment>
    {
        public void Configure(EntityTypeBuilder<UnitAssetAssignment> builder)
        {
            builder.ToTable("UnitAssetAssignments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AssignedAt)
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasMaxLength(1000);

            builder.Property(x => x.IsPrimary)
                .HasDefaultValue(false);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.HasOne(x => x.Unit)
                .WithMany(x => x.AssetAssignments)
                .HasForeignKey(x => x.UnitId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProjectAsset)
                .WithMany(x => x.UnitAssignments)
                .HasForeignKey(x => x.ProjectAssetId)
                .OnDelete(DeleteBehavior.Restrict);

            // אותה הצמדה לא תשויך לאותה דירה פעמיים
            builder.HasIndex(x => new
            {
                x.UnitId,
                x.ProjectAssetId
            }).IsUnique();
        }
    }
}
