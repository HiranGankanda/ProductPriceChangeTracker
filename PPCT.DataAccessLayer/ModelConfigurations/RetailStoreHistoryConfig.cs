using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PPCT.DataAccessLayer.DataModels.ProjectTableModels;

namespace PPCT.DataAccessLayer.ModelConfigurations
{
    public class RetailStoreHistoryConfig : IEntityTypeConfiguration<RetailStore_History>
    {
        public void Configure(EntityTypeBuilder<RetailStore_History> builder)
        {
            builder.HasKey(c => c.HistoryId);
            builder.Property(c => c.HistoryId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(c => c.RetailStoreName).HasMaxLength(250).HasColumnType("varchar");
            builder.Property(c => c.ChangeNote).IsRequired().HasMaxLength(150).HasColumnType("varchar");
            builder.Property(c => c.CreatedBy).IsRequired().HasMaxLength(256).HasColumnType("nvarchar");
            builder.Property(c => c.CreatedOn).IsRequired().HasColumnType("datetime").HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
        }
    }
}