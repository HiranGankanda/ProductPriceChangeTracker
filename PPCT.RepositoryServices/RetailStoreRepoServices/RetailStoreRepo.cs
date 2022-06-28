using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PPCT.DataSupport;
using PPCT.DataSupport.DataModels.ProjectTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPCT.RepositoryServices.RetailStoreRepoServices
{
    //Repository layer is intended only to retrieve and set items in the data store.
    public class RetailStoreRepo : IRetailStoreRepo
    {
        private readonly DatabaseContext _context;
        private readonly ILogger _logger;
        public RetailStoreRepo(DatabaseContext context, ILogger<RetailStoreRepo> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<bool> CreateNewRetailStoreAsync(RetailStore RetailStoreDetails)
        {
            _logger.LogInformation($"[RetailStoreRepo]CreateNewRetailStore(RetailStoreDetails) hit at {DateTime.UtcNow.ToLongTimeString()}");

            try
            {
                bool available = _context.RetailStores.Any(c => c.RetailStoreName == RetailStoreDetails.RetailStoreName);
                if (available == true)
                {
                    _logger.LogInformation($"[RetailStoreRepo]CreateNewRetailStore(RetailStoreDetails) [RETAIL-STORE ALREADY CREATED] hit at {DateTime.UtcNow.ToLongTimeString()}");
                    return false;
                }
                else
                {
                    _context.RetailStores.Add(RetailStoreDetails);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"[RetailStoreRepo]CreateNewRetailStore(RetailStoreDetails) [SUCCESS] hit at {DateTime.UtcNow.ToLongTimeString()}");

                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error occurred while CreateNewRetailStore(RetailStore RetailStoreDetails), Exception : {1}", ex.ToString());
                return false;
            }
        }
        public async Task<bool> UpdateRetailStoreAsync(RetailStore RetailStoreDetails)
        {
            _logger.LogInformation($"[RetailStoreRepo]UpdateRetailStore() hit at {DateTime.UtcNow.ToLongTimeString()}");
            try
            {
                _context.Entry(RetailStoreDetails).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error occurred while UpdateRetailStore(), Exception : {1}", ex.ToString());
                return false;
            }
        }
        public async Task<bool> DeleteRetailStoreAsync(int Id)
        {
            _logger.LogInformation($"[RetailStoreRepo]DeleteRetailStore(int id) hit at {DateTime.UtcNow.ToLongTimeString()}");
            try
            {
                var retailStore = await _context.RetailStores.FindAsync(Id);
                if (retailStore == null)
                {
                    _logger.LogInformation($"[RetailStoreRepo]DeleteRetailStore(Id) [RETAILSTORE NOT FOUND] hit at {DateTime.UtcNow.ToLongTimeString()}");
                    return false;
                }
                else
                {
                    _context.RetailStores.Remove(retailStore);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"[RetailStoreRepo]DeleteRetailStore(Id) [RETAILSTORE DELETED SUCCESSFULLY] hit at {DateTime.UtcNow.ToLongTimeString()}");

                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error occurred while DeleteRetailStore(int Id), Exception : {1}", ex.ToString());
                return false;
            }
        }
        public async Task<List<RetailStore>> GetRetailStoresListAsync()
        {
            _logger.LogInformation($"[RetailStoreRepo]GetRetailStoresListAsync() hit at {DateTime.UtcNow.ToLongTimeString()}");
            try
            {
                return await _context.RetailStores.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error occurred while GetRetailStoresListAsync(), Exception : {1}", ex.ToString());
                return null;
            }
        }
        public async Task<RetailStore> FindRetailStoreDetailsAsync(int Id)
        {
            _logger.LogInformation($"[RetailStoreRepo]FindRetailStoreDetails(int id) hit at {DateTime.UtcNow.ToLongTimeString()}");
            try
            {
                return await _context.RetailStores.FindAsync(Id);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error occurred while FindRetailStoreDetails(int Id), Exception : {1}", ex.ToString());
                return null;
            }
        }
    }
}
