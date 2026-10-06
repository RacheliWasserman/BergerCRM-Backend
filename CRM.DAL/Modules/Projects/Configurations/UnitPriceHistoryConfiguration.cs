using System;
using System.Collections.Generic;
using System.Text;

using CRM.DAL.Modules.Projects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DAL.Modules.Projects.Configurations
{
    public class UnitPriceHistoryConfiguration
        : IEntityTypeConfiguration<UnitPriceHistory>
    {
        public void Configure(EntityTypeBuilder<UnitPriceHistory> builder)
        {
            builder.ToTable("UnitPriceHistory");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OldPrice)
                .HasPrecision(18, 2);

            builder.Property(x => x.NewPrice)
                .HasPrecision(18, 2);

            builder.Property(x => x.OldPricePerSquareMeter)
                .HasPrecision(18, 2);

            builder.Property(x => x.NewPricePerSquareMeter)
                .HasPrecision(18, 2);

            builder.Property(x => x.Reason)
                .HasMaxLength(500);

            builder.Property(x => x.Notes)
                .HasMaxLength(1000);

            builder.HasOne(x => x.Unit)
                .WithMany(x => x.PriceHistory)
                .HasForeignKey(x => x.UnitId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ChangedByUser)
                .WithMany()
                .HasForeignKey(x => x.ChangedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.UnitId);

            builder.HasIndex(x => x.ChangedAt);
        }
    }
}
