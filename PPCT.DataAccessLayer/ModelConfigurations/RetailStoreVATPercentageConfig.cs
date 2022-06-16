using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PPCT.DataAccessLayer.DataModels.ProjectTableModels;

namespace PPCT.DataAccessLayer.ModelConfigurations
{
    public class RetailStoreVATPercentageConfig : IEntityTypeConfiguration<RetailStoreVATPercentage>
    {
        public void Configure(EntityTypeBuilder<RetailStoreVATPercentage> builder)
        {
            builder.HasKey(v => v.VATPercentageId);
            builder.Property(v => v.VATPercentageId).IsRequired().ValueGeneratedOnAdd();

            builder.Property(v => v.RetailStoreID).IsRequired();
            builder.Property(v => v.VATPercentageValue).IsRequired();
            builder.Property(v => v.IsActive).IsRequired().HasDefaultValue(true);
        }
    }
}
