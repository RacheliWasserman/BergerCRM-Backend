using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CRM.DAL.Modules.Deals.Entities;

namespace CRM.DAL.Modules.Deals.Configurations
{
    public class DealConfiguration : IEntityTypeConfiguration<Deal>
    {
        public void Configure(EntityTypeBuilder<Deal> builder)
        {
            builder.ToTable("Deals");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DealNumber).IsRequired().HasMaxLength(50);
            builder.HasIndex(x => x.DealNumber).IsUnique();

            builder.Property(x => x.ExpectedAmount).HasPrecision(18, 2);
            builder.Property(x => x.Notes).HasMaxLength(2000);

            builder.Property(x => x.OpenedAt).IsRequired();
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.HasIndex(x => x.LeadId);
            builder.HasIndex(x => x.ContactId);
            builder.HasIndex(x => x.CustomerId);
            builder.HasIndex(x => x.ProjectId);
            builder.HasIndex(x => x.UnitId);
            builder.HasIndex(x => x.DealStageId);
            builder.HasIndex(x => x.OwnerUserId);
            builder.HasIndex(x => x.OpenedAt);
            builder.HasIndex(x => x.IsActive);

            builder.HasOne(x => x.Lead)
                .WithMany()
                .HasForeignKey(x => x.LeadId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Contact)
                .WithMany()
                .HasForeignKey(x => x.ContactId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Project)
                .WithMany()
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Unit)
                .WithMany()
                .HasForeignKey(x => x.UnitId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.DealStage)
                .WithMany(x => x.Deals)
                .HasForeignKey(x => x.DealStageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.OwnerUser)
                .WithMany()
                .HasForeignKey(x => x.OwnerUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
