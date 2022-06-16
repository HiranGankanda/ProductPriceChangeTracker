using System.ComponentModel.DataAnnotations.Schema;

namespace PPCT.DataAccessLayer.DataModels.ProjectTableModels
{
    [Table("RetailStore")]
    public class RetailStore
    {
        public int RetailStoreId { get; set; }
        public string RetailStoreName { get; set; }
        public bool IsActive { get; set; }
    }
}
