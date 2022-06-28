using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPCT.DataSupport.DataModels.ProjectTableModels
{
    [Table("RetailStore_History")]
    public class RetailStore_History
    {
        public int? HistoryId { get; set; }
        public int? RetailStoreId { get; set; }
        public string RetailStoreName { get; set; }
        public bool? IsActive { get; set; }
        public string ChangeNote { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
