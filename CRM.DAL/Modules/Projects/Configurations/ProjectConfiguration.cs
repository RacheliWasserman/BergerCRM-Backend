using System;
using System.Collections.Generic;
using System.Text;

using CRM.DAL.Modules.Projects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DAL.Modules.Projects.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(x => x.Code)
                .IsUnique();

            builder.Property(x => x.Description)
                .HasMaxLength(2000);

            builder.Property(x => x.City)
                .HasMaxLength(100);

            builder.Property(x => x.Street)
                .HasMaxLength(150);

            builder.Property(x => x.Address)
                .HasMaxLength(300);

            builder.Property(x => x.PostalCode)
                .HasMaxLength(20);

            builder.Property(x => x.Country)
                .HasMaxLength(100);

            builder.Property(x => x.DeveloperName)
                .HasMaxLength(200);

            builder.Property(x => x.ContractorName)
                .HasMaxLength(200);

            builder.Property(x => x.LandArea)
                .HasPrecision(18, 2);

            builder.HasOne(x => x.ProjectType)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => x.ProjectTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProjectStatus)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => x.ProjectStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.HasOne(x => x.ProjectManagerUser)
                .WithMany()
                .HasForeignKey(x => x.ProjectManagerUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
