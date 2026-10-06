using System;
using System.Collections.Generic;
using System.Text;

using CRM.DAL.Modules.Projects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DAL.Modules.Projects.Configurations
{
    public class UnitConfiguration
        : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.ToTable("Units");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UnitNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.InternalCode)
                .HasMaxLength(100);

            builder.Property(x => x.NumberOfRooms)
                .HasPrecision(4, 1);

            builder.Property(x => x.BuiltArea)
                .HasPrecision(18, 2);

            builder.Property(x => x.BalconyArea)
                .HasPrecision(18, 2);

            builder.Property(x => x.GardenArea)
                .HasPrecision(18, 2);

            builder.Property(x => x.RoofArea)
                .HasPrecision(18, 2);

            builder.Property(x => x.TotalArea)
                .HasPrecision(18, 2);

            builder.Property(x => x.CurrentPrice)
                .HasPrecision(18, 2);

            builder.Property(x => x.PricePerSquareMeter)
                .HasPrecision(18, 2);

            builder.Property(x => x.Direction)
                .HasMaxLength(100);

            builder.Property(x => x.ViewDescription)
                .HasMaxLength(500);

            builder.Property(x => x.MarketingDescription)
                .HasMaxLength(2000);

            builder.Property(x => x.InternalNotes)
                .HasMaxLength(2000);

            // מספר דירה ייחודי בתוך אותה קומה
            builder.HasIndex(x => new
            {
                x.FloorId,
                x.UnitNumber
            }).IsUnique();

            builder.HasOne(x => x.Floor)
                .WithMany(x => x.Units)
                .HasForeignKey(x => x.FloorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.UnitType)
                .WithMany(x => x.Units)
                .HasForeignKey(x => x.UnitTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.UnitStatus)
                .WithMany(x => x.Units)
                .HasForeignKey(x => x.UnitStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}
