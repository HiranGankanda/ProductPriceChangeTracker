using System.ComponentModel.DataAnnotations.Schema;

namespace PPCT.DataAccessLayer.DataModels.ProjectTableModels
{
    [Table("RetailStoreMarginRecords")]
    public class RetailStoreMarginRecord
    {
        public int RetailStoreMarginRecordID { get; set; }
        public int RetailStoreID { get; set; }
        public int ProductID { get; set; }
        public double SpecialRetailPrice { get; set; }
        public double RetailStoreMargin { get; set; }
        public bool WithVAT { get; set; }
        public string Bouns { get; set; }
    }
}