using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CRM.DAL.Modules.Deals.Entities;

namespace CRM.DAL.Modules.Deals.Configurations
{
    public class DealStageConfiguration : IEntityTypeConfiguration<DealStage>
    {
        public void Configure(EntityTypeBuilder<DealStage> builder)
        {
            builder.ToTable("DealStages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Code).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.SortOrder).IsRequired();
            builder.Property(x => x.IsClosed).HasDefaultValue(false);
            builder.Property(x => x.IsWon).HasDefaultValue(false);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.HasIndex(x => x.Code).IsUnique();
        }
    }
}
