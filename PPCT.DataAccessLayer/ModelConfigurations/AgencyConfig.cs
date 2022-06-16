using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PPCT.DataAccessLayer.DataModels.ProjectTableModels;

namespace PPCT.DataAccessLayer.ModelConfigurations
{
    public class AgencyConfig : IEntityTypeConfiguration<Agency>
    {
        public void Configure(EntityTypeBuilder<Agency> builder)
        {
            builder.HasKey(a => a.AgencyID);
            builder.Property(a => a.AgencyID).IsRequired().ValueGeneratedOnAdd();
            builder.Property(a => a.AgencyName).IsRequired().HasMaxLength(250).HasColumnType("varchar");
        }
    }
}
