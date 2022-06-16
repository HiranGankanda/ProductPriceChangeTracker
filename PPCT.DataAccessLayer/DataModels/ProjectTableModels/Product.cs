using System.ComponentModel.DataAnnotations.Schema;

namespace PPCT.DataAccessLayer.DataModels.ProjectTableModels
{
    [Table("Products")]
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PackSize { get; set; }
        public int AgencyID { get; set; }
    }
}