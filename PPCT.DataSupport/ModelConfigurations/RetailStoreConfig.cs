using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PPCT.DataSupport.DataModels.ProjectTableModels;

namespace PPCT.DataSupport.ModelConfigurations
{
    public class RetailStoreConfig : IEntityTypeConfiguration<RetailStore>
    {
        public void Configure(EntityTypeBuilder<RetailStore> builder)
        {
            builder.HasKey(c => c.RetailStoreId);
            builder.Property(c => c.RetailStoreId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(c => c.RetailStoreName).IsRequired().HasMaxLength(250).HasColumnType("varchar");
            builder.Property(c => c.IsActive).IsRequired().HasDefaultValue(false);
        }
    }
}
