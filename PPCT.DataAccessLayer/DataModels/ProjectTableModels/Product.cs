using System.ComponentModel.DataAnnotations.Schema;

namespace PPCT.DataSupport.DataModels.ProjectTableModels
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