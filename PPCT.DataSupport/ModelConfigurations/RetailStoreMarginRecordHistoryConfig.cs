using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PPCT.DataSupport.DataModels.ProjectTableModels;

namespace PPCT.DataSupport.ModelConfigurations
{
    public class RetailStoreMarginRecordHistoryConfig : IEntityTypeConfiguration<RetailStoreMarginRecord_History>
    {
        public void Configure(EntityTypeBuilder<RetailStoreMarginRecord_History> builder)
        {
            builder.HasKey(m => m.HistoryID);
            builder.Property(m => m.HistoryID).IsRequired().ValueGeneratedOnAdd();
            builder.Property(m => m.Bouns).HasMaxLength(250).HasColumnType("varchar");
            builder.Property(m => m.ChangeNote).IsRequired().HasMaxLength(150).HasColumnType("varchar");
            builder.Property(m => m.ChangedBy).IsRequired().HasMaxLength(50).HasColumnType("varchar");
            builder.Property(m => m.ChangedOn).IsRequired().HasColumnType("datetime").HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
        }
    }
}