using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CRM.DAL.Modules.Customers.Entities;

namespace CRM.DAL.Modules.Customers.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CustomerNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(x => x.CustomerNumber).IsUnique();

            builder.Property(x => x.DisplayName)
                .HasMaxLength(200);

            builder.Property(x => x.Notes)
                .HasMaxLength(2000);

            builder.Property(x => x.CustomerSince)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.HasIndex(x => x.CustomerTypeId);
            builder.HasIndex(x => x.CustomerStatusId);
            builder.HasIndex(x => x.IsActive);

            builder.HasOne(x => x.CustomerType)
                .WithMany(x => x.Customers)
                .HasForeignKey(x => x.CustomerTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CustomerStatus)
                .WithMany(x => x.Customers)
                .HasForeignKey(x => x.CustomerStatusId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
