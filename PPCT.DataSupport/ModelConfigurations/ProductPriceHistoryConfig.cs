using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PPCT.DataSupport.DataModels.ProjectTableModels;

namespace PPCT.DataSupport.ModelConfigurations
{
    public class ProductPriceHistoryConfig : IEntityTypeConfiguration<ProductPrice_History>
    {
        public void Configure(EntityTypeBuilder<ProductPrice_History> builder)
        {
            builder.HasKey(p => p.HistoryId);
            builder.Property(p => p.HistoryId).IsRequired().ValueGeneratedOnAdd();

            builder.Property(p => p.ChangeNote).IsRequired().HasMaxLength(150).HasColumnType("varchar");
            builder.Property(p => p.CreatedBy).IsRequired().HasMaxLength(256).HasColumnType("nvarchar");
            builder.Property(p => p.CreatedOn).IsRequired().HasColumnType("datetime").HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
        }
    }
}