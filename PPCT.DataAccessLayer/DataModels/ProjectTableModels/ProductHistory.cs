using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPCT.DataSupport.DataModels.ProjectTableModels
{
    [Table("Products_History")]
    public class Product_History
    {
        public int HistoryId { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public string PackSize { get; set; }
        public int? AgencyID { get; set; }
        public int? BrandID { get; set; }
        public int? PLU { get; set; }
        public string ChangeNote { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
