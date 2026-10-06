using System;
using System.Collections.Generic;
using System.Text;

using CRM.DAL.Modules.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DAL.Modules.Identity.Configurations
{
    public class UserConfiguration
        : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmployeeNumber)
                .HasMaxLength(50);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.NormalizedEmail)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.Phone)
                .HasMaxLength(30);

            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.JobTitle)
                .HasMaxLength(150);

            builder.Property(x => x.ProfileImageUrl)
                .HasMaxLength(1000);

            builder.Property(x => x.PreferredLanguage)
                .HasMaxLength(20);

            builder.Property(x => x.TimeZone)
                .HasMaxLength(100);

            // אימייל חייב להיות ייחודי
            builder.HasIndex(x => x.NormalizedEmail)
                .IsUnique();

            // מספר עובד - אם קיים
            builder.HasIndex(x => x.EmployeeNumber)
                .IsUnique();

            builder.HasIndex(x => x.DepartmentId);

            builder.HasIndex(x => x.ManagerUserId);

            builder.HasIndex(x => x.IsActive);

            // המשתמש שייך למחלקה
            builder.HasOne(x => x.Department)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);

            // מנהל ישיר של המשתמש
            builder.HasOne(x => x.ManagerUser)
                .WithMany(x => x.ManagedUsers)
                .HasForeignKey(x => x.ManagerUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.IsLocked)
                .HasDefaultValue(false);

            builder.Property(x => x.FailedLoginAttempts)
                .HasDefaultValue(0);

            builder.Property(x => x.MustChangePassword)
                .HasDefaultValue(false);

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.HasOne(x => x.DefaultProject)
                .WithMany()
                .HasForeignKey(x => x.DefaultProjectId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}