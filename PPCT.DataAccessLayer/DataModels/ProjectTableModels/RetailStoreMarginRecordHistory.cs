using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPCT.DataSupport.DataModels.ProjectTableModels
{
    [Table("RetailStoreMarginRecords_History")]
    public class RetailStoreMarginRecord_History
    {
        public int HistoryID { get; set; }
        public int RetailStoreMarginRecordID { get; set; }
        public int RetailStoreID { get; set; }
        public int ProductID { get; set; }
        public double SpecialRetailPrice { get; set; }
        public double RetailStoreMargin { get; set; }
        public bool WithVAT { get; set; }
        public string Bouns { get; set; }
        public string ChangeNote { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedOn { get; set; }
    }
}
