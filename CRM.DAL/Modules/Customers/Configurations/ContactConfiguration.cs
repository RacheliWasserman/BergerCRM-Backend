using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CRM.DAL.Modules.Customers.Entities;

namespace CRM.DAL.Modules.Customers.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contacts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .HasMaxLength(256);

            builder.Property(x => x.NormalizedEmail)
                .HasMaxLength(256);

            builder.Property(x => x.Phone)
                .HasMaxLength(30);

            builder.Property(x => x.SecondaryPhone)
                .HasMaxLength(30);

            builder.Property(x => x.IdentificationNumber)
                .HasMaxLength(50);

            builder.Property(x => x.Address)
                .HasMaxLength(300);

            builder.Property(x => x.City)
                .HasMaxLength(100);

            builder.Property(x => x.PostalCode)
                .HasMaxLength(20);

            builder.Property(x => x.Country)
                .HasMaxLength(100);

            builder.Property(x => x.PreferredLanguage)
                .HasMaxLength(20);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.HasIndex(x => x.NormalizedEmail);
            builder.HasIndex(x => x.Phone);
            builder.HasIndex(x => x.IdentificationNumber);
            builder.HasIndex(x => x.IsActive);
        }
    }
}
