using System;
using System.Collections.Generic;
using System.Text;

using CRM.DAL.Modules.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DAL.Modules.Identity.Configurations
{
    public class LoginHistoryConfiguration
        : IEntityTypeConfiguration<LoginHistory>
    {
        public void Configure(EntityTypeBuilder<LoginHistory> builder)
        {
            builder.ToTable("LoginHistory");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmailAttempted)
                .HasMaxLength(256);

            builder.Property(x => x.IpAddress)
                .HasMaxLength(100);

            builder.Property(x => x.UserAgent)
                .HasMaxLength(1000);

            builder.Property(x => x.Device)
                .HasMaxLength(100);

            builder.Property(x => x.Browser)
                .HasMaxLength(100);

            builder.Property(x => x.OperatingSystem)
                .HasMaxLength(100);

            builder.Property(x => x.FailureReason)
                .HasMaxLength(500);

            builder.HasOne(x => x.User)
                .WithMany(x => x.LoginHistories)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.UserId);

            builder.HasIndex(x => x.LoginAt);

            builder.HasIndex(x => x.WasSuccessful);
        }
    }
}
