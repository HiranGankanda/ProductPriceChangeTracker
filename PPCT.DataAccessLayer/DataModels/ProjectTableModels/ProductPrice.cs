using System.ComponentModel.DataAnnotations.Schema;

namespace PPCT.DataAccessLayer.DataModels.ProjectTableModels
{
    [Table("ProductPrices")]
    public class ProductPrice
    {
        public int ProductPriceId { get; set; }
        public int ProductId { get; set; }
        public double RetailPrice { get; set; }
        public bool IsActive { get; set; }
    }
}