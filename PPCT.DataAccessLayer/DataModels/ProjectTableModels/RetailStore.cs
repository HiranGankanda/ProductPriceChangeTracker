using System.ComponentModel.DataAnnotations.Schema;

namespace PPCT.DataSupport.DataModels.ProjectTableModels
{
    [Table("RetailStore")]
    public class RetailStore
    {
        public int RetailStoreId { get; set; }
        public string RetailStoreName { get; set; }
        public bool IsActive { get; set; }
    }
}
