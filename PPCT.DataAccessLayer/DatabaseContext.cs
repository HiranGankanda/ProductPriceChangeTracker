using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PPCT.DataAccessLayer.DataModels.ProjectTableModels;
using PPCT.DataAccessLayer.ModelConfigurations;

namespace PPCT.DataAccessLayer
{
    //public class DatabaseContext:DbContext
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        #region Constructor
        private readonly DbContextOptions _options;
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            _options = options;
        }
        #endregion

        //Main Table Class Objects
        #region DB-Tables
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<AgencyHistory> AgenciesHistory { get; set; }
        public DbSet<RetailStore> RetailStores { get; set; }
        public DbSet<RetailStore_History> RetailStores_History { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Product_History> Products_History { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<ProductPrice_History> ProductPrices_History { get; set; }
        public DbSet<RetailStoreVATPercentage> VATPercentages { get; set; }
        public DbSet<RetailStoreVATPercentage_History> VATPercentages_History { get; set; }
        public DbSet<RetailStoreMarginRecord> CompanyMarginRecords { get; set; }
        public DbSet<RetailStoreMarginRecord_History> CompanyMarginRecords_History { get; set; }
        #endregion


        private const string OfflineConnectionString = @"Server=DESKTOP-GB7NE15;Database=ProductPriceChangesDB;User ID=sa;password=hiran@123;Integrated Security=SSPI;Trusted_Connection=True;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(OfflineConnectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Fluent API
            builder.ApplyConfiguration(new AgencyConfig());
            builder.ApplyConfiguration(new AgencyHistoryConfig());

            builder.ApplyConfiguration(new RetailStoreConfig());
            builder.ApplyConfiguration(new RetailStoreHistoryConfig());

            builder.ApplyConfiguration(new RetailStoreMarginRecordConfig());
            builder.ApplyConfiguration(new RetailStoreMarginRecordHistoryConfig());

            builder.ApplyConfiguration(new ProductConfig());
            builder.ApplyConfiguration(new ProductHistoryConfig());

            builder.ApplyConfiguration(new ProductPriceConfig());
            builder.ApplyConfiguration(new ProductPriceHistoryConfig());

            builder.ApplyConfiguration(new RetailStoreVATPercentageConfig());
            builder.ApplyConfiguration(new RetailStoreVATPercentageHistoryConfig());

            base.OnModelCreating(builder);
        }
    }
}