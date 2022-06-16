using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PPCT.DataAccessLayer.DataModels.ProjectTableModels;

namespace PPCT.DataAccessLayer.ModelConfigurations
{
    public class RetailStoreVATPercentageHistoryConfig : IEntityTypeConfiguration<RetailStoreVATPercentage_History>
    {
        public void Configure(EntityTypeBuilder<RetailStoreVATPercentage_History> builder)
        {
            builder.HasKey(v => v.HistoryId);
            builder.Property(v => v.HistoryId).IsRequired().ValueGeneratedOnAdd();

            builder.Property(v => v.ChangeNote).IsRequired().HasMaxLength(150).HasColumnType("varchar");
            builder.Property(v => v.CreatedBy).IsRequired().HasMaxLength(256).HasColumnType("nvarchar");
            builder.Property(v => v.CreatedOn).IsRequired().HasColumnType("datetime").HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
        }
    }
}
