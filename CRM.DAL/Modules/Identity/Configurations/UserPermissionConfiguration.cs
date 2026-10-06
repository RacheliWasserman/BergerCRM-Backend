using System;
using System.Collections.Generic;
using System.Text;
using CRM.DAL.Modules.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DAL.Modules.Identity.Configurations
{
    public class UserPermissionConfiguration
        : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.ToTable("UserPermissions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Reason)
                .HasMaxLength(500);

            builder.Property(x => x.IsAllowed)
                .HasDefaultValue(true);

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserPermissions)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Permission)
                .WithMany(x => x.UserPermissions)
                .HasForeignKey(x => x.PermissionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.UserId,
                x.PermissionId
            }).IsUnique();
        }
    }
}
