using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PPCT.DataSupport.DataModels.ProjectTableModels;

namespace PPCT.DataSupport.ModelConfigurations
{
    public class ProductPriceConfig : IEntityTypeConfiguration<ProductPrice>
    {
        public void Configure(EntityTypeBuilder<ProductPrice> builder)
        {
            builder.HasKey(p => p.ProductPriceId);
            builder.Property(p => p.ProductPriceId).IsRequired().ValueGeneratedOnAdd();

            builder.Property(p => p.ProductId).IsRequired();

            builder.Property(p => p.RetailPrice).IsRequired();

            builder.Property(p => p.IsActive).IsRequired().HasDefaultValue(true);
        }
    }
}