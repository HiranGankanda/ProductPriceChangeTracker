using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PPCT.DataAccessLayer.DataModels.ProjectTableModels;

namespace PPCT.DataAccessLayer.ModelConfigurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);
            builder.Property(p => p.ProductId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.ProductName).IsRequired().HasMaxLength(250).HasColumnType("varchar");
            builder.Property(p => p.PackSize).IsRequired().HasMaxLength(100).HasColumnType("varchar");
            builder.Property(p => p.AgencyID).IsRequired().HasDefaultValue(0);
        }
    }
}