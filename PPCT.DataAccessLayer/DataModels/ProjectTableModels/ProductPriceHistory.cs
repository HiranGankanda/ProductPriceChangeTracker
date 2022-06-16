using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPCT.DataAccessLayer.DataModels.ProjectTableModels
{
    [Table("ProductPrices_History")]
    public class ProductPrice_History
    {
        public int HistoryId { get; set; }
        public int? ProductPriceId { get; set; }
        public int? ProductId { get; set; }
        public double? RetailPrice { get; set; }
        public bool? IsActive { get; set; }
        public string ChangeNote { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
