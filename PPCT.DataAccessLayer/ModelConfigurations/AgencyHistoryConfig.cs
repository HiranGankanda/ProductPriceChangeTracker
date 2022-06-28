using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PPCT.DataSupport.DataModels.ProjectTableModels;

namespace PPCT.DataSupport.ModelConfigurations
{
    public class AgencyHistoryConfig : IEntityTypeConfiguration<AgencyHistory>
    {
        public void Configure(EntityTypeBuilder<AgencyHistory> builder)
        {
            builder.HasKey(a => a.AgencyID);
            builder.Property(a => a.AgencyID).IsRequired().ValueGeneratedOnAdd();
            builder.Property(a => a.AgencyName).HasMaxLength(250).HasColumnType("varchar");
            builder.Property(a => a.ChangeNote).IsRequired().HasMaxLength(150).HasColumnType("varchar");
            builder.Property(a => a.ChangedBy).IsRequired().HasMaxLength(256).HasColumnType("nvarchar");
            builder.Property(a => a.ChangedOn).IsRequired().HasColumnType("datetime").HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
        }
    }
}
