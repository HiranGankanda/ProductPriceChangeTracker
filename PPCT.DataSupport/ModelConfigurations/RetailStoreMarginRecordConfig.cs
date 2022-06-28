using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PPCT.DataSupport.DataModels.ProjectTableModels;

namespace PPCT.DataSupport.ModelConfigurations
{
    public class RetailStoreMarginRecordConfig : IEntityTypeConfiguration<RetailStoreMarginRecord>
    {
        public void Configure(EntityTypeBuilder<RetailStoreMarginRecord> builder)
        {
            builder.HasKey(m => m.RetailStoreMarginRecordID);

            builder.Property(m => m.RetailStoreMarginRecordID).IsRequired().ValueGeneratedOnAdd();
            builder.Property(m => m.RetailStoreID).IsRequired();
            builder.Property(m => m.ProductID).IsRequired();
            builder.Property(m => m.SpecialRetailPrice).IsRequired();
            builder.Property(m => m.WithVAT).IsRequired().HasDefaultValue(false);
            builder.Property(m => m.RetailStoreMargin).IsRequired();
            builder.Property(m => m.Bouns).HasMaxLength(250).HasColumnType("varchar");
        }
    }
}