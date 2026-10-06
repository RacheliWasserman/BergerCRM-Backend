using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CRM.DAL.Modules.Customers.Entities;

namespace CRM.DAL.Modules.Customers.Configurations
{
    public class CustomerContactConfiguration : IEntityTypeConfiguration<CustomerContact>
    {
        public void Configure(EntityTypeBuilder<CustomerContact> builder)
        {
            builder.ToTable("CustomerContacts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.RelationshipType)
                .HasMaxLength(100);

            builder.Property(x => x.IsPrimary)
                .HasDefaultValue(false);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.LinkedAt)
                .IsRequired();

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.HasIndex(x => new { x.CustomerId, x.ContactId }).IsUnique();
            builder.HasIndex(x => x.ContactId);
            builder.HasIndex(x => x.CustomerId);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.CustomerContacts)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Contact)
                .WithMany(x => x.CustomerContacts)
                .HasForeignKey(x => x.ContactId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
