using PPCT.DataAccessLayer.DataModels.ProjectTableModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPCT.RepositoryServices.ProductRepoService
{
    public interface IProductRepo
    {
        Task<bool> CreateNewProductAsync(Product ProductDetails);
        Task<bool> UpdateProductAsync(Product ProductDetails);
        Task<bool> DeleteProductAsync(int Id);
        Task<List<Product>> GetProductsListAsync();
        Task<Product> FindProductDetailsAsync(int Id);
    }
}