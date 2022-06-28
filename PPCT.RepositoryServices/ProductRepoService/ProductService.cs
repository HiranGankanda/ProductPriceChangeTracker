using PPCT.DataSupport.DataModels.ProjectTableModels;
using PPCT.RepositoryServices.RetailStoreRepoServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPCT.RepositoryServices.ProductRepoService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _Repo;
        public ProductService(IProductRepo Repo)
        {
            _Repo = Repo;
        }

        //Main Methodes
        public async Task<bool> CreateNewProductServiceAsync(Product ProductDetails) => await _Repo.CreateNewProductAsync(ProductDetails);
        public async Task<bool> UpdateProductServiceAsync(Product ProductDetails) => await _Repo.UpdateProductAsync(ProductDetails);
        public async Task<bool> DeleteProductServiceAsync(int Id) => await _Repo.DeleteProductAsync(Id);
        public async Task<List<Product>> GetProductsListServiceAsync() => await _Repo.GetProductsListAsync();
        public async Task<Product> FindProductDetailsServiceAsync(int Id) => await _Repo.FindProductDetailsAsync(Id);
    }
}