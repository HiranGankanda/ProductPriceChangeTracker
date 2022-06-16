using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PPCT.DataAccessLayer.DataModels.ProjectTableModels;

namespace PPCT.DataAccessLayer.ModelConfigurations
{
    public class ProductHistoryConfig : IEntityTypeConfiguration<Product_History>
    {
        public void Configure(EntityTypeBuilder<Product_History> builder)
        {
            builder.HasKey(p => p.HistoryId);
            builder.Property(p => p.HistoryId).IsRequired().ValueGeneratedOnAdd();

            builder.Property(p => p.ProductName).HasMaxLength(250).HasColumnType("varchar");
            builder.Property(p => p.PackSize).HasMaxLength(100).HasColumnType("varchar");

            builder.Property(p => p.ChangeNote).IsRequired().HasMaxLength(150).HasColumnType("varchar");
            builder.Property(p => p.CreatedBy).IsRequired().HasMaxLength(256).HasColumnType("nvarchar");
            builder.Property(p => p.CreatedOn).IsRequired().HasColumnType("datetime").HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
        }
    }
}