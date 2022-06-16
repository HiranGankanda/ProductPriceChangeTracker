using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PPCT.DataAccessLayer;
using PPCT.DataAccessLayer.DataModels.ProjectTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPCT.RepositoryServices.ProductRepoService
{
    public class ProductRepo : IProductRepo
    {
        private readonly DatabaseContext _context;
        private readonly ILogger _logger;
        public ProductRepo(DatabaseContext context, ILogger<ProductRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateNewProductAsync(Product ProductDetails)
        {
            _logger.LogInformation($"[ProductRepo]CreateNewProductAsync(ProductDetails) hit at {DateTime.UtcNow.ToLongTimeString()}");

            try
            {
                bool available = _context.Products.Any(p => p.ProductName == ProductDetails.ProductName);
                if (available == true)
                {
                    _logger.LogInformation($"[ProductRepo]CreateNewProductAsync(ProductDetails) [PRODUCT ALREADY CREATED] hit at {DateTime.UtcNow.ToLongTimeString()}");
                    return false;
                }
                else
                {
                    _context.Products.Add(ProductDetails);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"[ProductRepo]CreateNewProductAsync(ProductDetails) [SUCCESS] hit at {DateTime.UtcNow.ToLongTimeString()}");

                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error occurred while CreateNewProductAsync(ProductDetails), Exception : {1}", ex.ToString());
                return false;
            }
        }
        public async Task<bool> UpdateProductAsync(Product ProductDetails)
        {
            _logger.LogInformation($"[ProductRepo]UpdateProductAsync() hit at {DateTime.UtcNow.ToLongTimeString()}");
            try
            {
                _context.Entry(ProductDetails).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error occurred while UpdateProductAsync(), Exception : {1}", ex.ToString());
                return false;
            }
        }
        public async Task<bool> DeleteProductAsync(int Id)
        {
            _logger.LogInformation($"[ProductRepo]DeleteProductAsync(int Id) hit at {DateTime.UtcNow.ToLongTimeString()}");
            try
            {
                var product = await _context.Products.FindAsync(Id);
                if (product == null)
                {
                    _logger.LogInformation($"[ProductRepo]DeleteProductAsync(Id) [PRODUCT NOT FOUND] hit at {DateTime.UtcNow.ToLongTimeString()}");
                    return false;
                }
                else
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"[ProductRepo]DeleteProductAsync(Id) [PRODUCT DELETED SUCCESSFULLY] hit at {DateTime.UtcNow.ToLongTimeString()}");

                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error occurred while DeleteProductAsync(int Id), Exception : {1}", ex.ToString());
                return false;
            }
        }
        public async Task<List<Product>> GetProductsListAsync()
        {
            _logger.LogInformation($"[ProductRepo]GetProductsListAsync() hit at {DateTime.UtcNow.ToLongTimeString()}");
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error occurred while GetProductsListAsync(), Exception : {1}", ex.ToString());
                return null;
            }
        }
        public async Task<Product> FindProductDetailsAsync(int Id)
        {
            _logger.LogInformation($"[ProductRepo]FindProductDetailsAsync(int id) hit at {DateTime.UtcNow.ToLongTimeString()}");
            try
            {
                return await _context.Products.FindAsync(Id);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error occurred while FindProductDetailsAsync(int Id), Exception : {1}", ex.ToString());
                return null;
            }
        }
    }
}
