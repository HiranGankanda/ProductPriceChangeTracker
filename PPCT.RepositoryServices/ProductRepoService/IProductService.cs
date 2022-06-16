using PPCT.DataAccessLayer.DataModels.ProjectTableModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPCT.RepositoryServices.ProductRepoService
{
    public interface IProductService
    {
        Task<bool> CreateNewProductServiceAsync(Product ProductDetails);
        Task<bool> UpdateProductServiceAsync(Product ProductDetails);
        Task<bool> DeleteProductServiceAsync(int Id);
        Task<List<Product>> GetProductsListServiceAsync();
        Task<Product> FindProductDetailsServiceAsync(int Id);
    }
}
