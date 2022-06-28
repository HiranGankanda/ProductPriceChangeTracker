using System.ComponentModel.DataAnnotations.Schema;

namespace PPCT.DataSupport.DataModels.ProjectTableModels
{
    [Table("RetailStoreVATPercentages")]
    public class RetailStoreVATPercentage
    {
        public int VATPercentageId { get; set; }
        public int RetailStoreID { get; set; }
        public double VATPercentageValue { get; set; }
        public bool IsActive { get; set; }
    }
}