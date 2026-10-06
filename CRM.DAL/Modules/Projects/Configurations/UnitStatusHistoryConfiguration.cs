using System;
using System.Collections.Generic;
using System.Text;

using CRM.DAL.Modules.Projects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DAL.Modules.Projects.Configurations
{
    public class UnitStatusHistoryConfiguration
        : IEntityTypeConfiguration<UnitStatusHistory>
    {
        public void Configure(EntityTypeBuilder<UnitStatusHistory> builder)
        {
            builder.ToTable("UnitStatusHistory");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Reason)
                .HasMaxLength(500);

            builder.Property(x => x.Notes)
                .HasMaxLength(1000);

            builder.HasOne(x => x.Unit)
                .WithMany(x => x.StatusHistory)
                .HasForeignKey(x => x.UnitId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.UnitStatus)
                .WithMany(x => x.UnitStatusHistories)
                .HasForeignKey(x => x.UnitStatusId)
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
