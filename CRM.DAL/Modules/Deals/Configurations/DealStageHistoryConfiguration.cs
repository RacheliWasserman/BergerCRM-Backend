using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CRM.DAL.Modules.Deals.Entities;

namespace CRM.DAL.Modules.Deals.Configurations
{
    public class DealStageHistoryConfiguration : IEntityTypeConfiguration<DealStageHistory>
    {
        public void Configure(EntityTypeBuilder<DealStageHistory> builder)
        {
            builder.ToTable("DealStageHistory");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ChangedAt).IsRequired();

            builder.Property(x => x.Reason).HasMaxLength(500);
            builder.Property(x => x.Notes).HasMaxLength(1000);

            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.HasIndex(x => x.DealId);
            builder.HasIndex(x => x.ChangedAt);

            builder.HasOne(x => x.Deal)
                .WithMany(x => x.StageHistory)
                .HasForeignKey(x => x.DealId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.DealStage)
                .WithMany(x => x.DealStageHistories)
                .HasForeignKey(x => x.DealStageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ChangedByUser)
                .WithMany()
                .HasForeignKey(x => x.ChangedByUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
