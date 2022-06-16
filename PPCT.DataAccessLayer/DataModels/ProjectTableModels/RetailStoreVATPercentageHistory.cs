using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPCT.DataAccessLayer.DataModels.ProjectTableModels
{
    [Table("RetailStoreVATPercentages_History")]
    public class RetailStoreVATPercentage_History
    {
        public int HistoryId { get; set; }
        public int? VATPercentageId { get; set; }
        public int? RetailStoreID { get; set; }
        public double? VATPercentageValue { get; set; }
        public bool? IsActive { get; set; }
        public string ChangeNote { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
