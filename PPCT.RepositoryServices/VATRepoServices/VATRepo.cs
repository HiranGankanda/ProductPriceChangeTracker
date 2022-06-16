using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PPCT.DataAccessLayer;
using PPCT.DataAccessLayer.DataModels.ProjectTableModels;
using PPCT.RepositoryServices.Helpers.HistoryRecordCreators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPCT.RepositoryServices.VATRepoServices
{
    public class VATRepo : IVATRepo
    {
        private readonly DatabaseContext _context;
        private readonly ILogger _logger;
        private readonly RetailStoreVATPercentageHistoryRecorder _recorder;
        private DatabaseContext context;

        public VATRepo(DatabaseContext context, ILogger<VATRepo> logger, RetailStoreVATPercentageHistoryRecorder recorder)
        {
            _context = context;
            _logger = logger;
            _recorder = recorder;
        }

        public VATRepo(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateNewVATPriceAsync(RetailStoreVATPercentage VATDetails, string User)
        {
            _logger.LogInformation($"[VATRepo]CreateNewVATPriceAsync(VATDetails) hit at {DateTime.UtcNow.ToLongTimeString()}");

            try
            {
                bool available = _context.VATPercentages.Any(
                    v => v.VATPercentageValue == VATDetails.VATPercentageValue 
                    && v.IsActive == true 
                    && v.RetailStoreID == VATDetails.RetailStoreID);
                if (available == true)
                {
                    _logger.LogInformation($"[VATRepo]CreateNewVATPriceAsync(VATDetails) [VAT PRICE ALREADY CREATED] hit at {DateTime.UtcNow.ToLongTimeString()}");
                    return false;
                }
                else
                {
                    RetailStoreVATPercentage hasActivePrice = _context.VATPercentages.FirstOrDefault(v => 
                        v.RetailStoreID == VATDetails.RetailStoreID && v.IsActive == VATDetails.IsActive);
                    if(hasActivePrice is not null)
                    {
                        hasActivePrice.IsActive = false;
                        await _context.SaveChangesAsync();
                        _logger.LogInformation($"[VATRepo]CreateNewVATPriceAsync(VATDetails) [OLD VAT PRICE UPDATED] hit at {DateTime.UtcNow.ToLongTimeString()}");
                    }

                    _context.VATPercentages.Add(VATDetails);                    
                    await _context.SaveChangesAsync();

                    //Use two SaveChangesAsync to get "Id" for history table
                    _context.VATPercentages_History.Add(_recorder.VATPercentageHistoryCreator(VATDetails, "Create New Record", User));
                    await _context.SaveChangesAsync();

                    _logger.LogInformation($"[VATRepo]CreateNewVATPriceAsync(VATDetails) [SUCCESS] hit at {DateTime.UtcNow.ToLongTimeString()}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error occurred while CreateNewVATPriceAsync(VATDetails), Exception : {1}", ex.ToString());
                return false;
            }
        }
        public async Task<bool> UpdateVATPriceAsync(RetailStoreVATPercentage VATDetails, string User)
        {
            _logger.LogInformation($"[VATRepo]UpdateVATPriceAsync() hit at {DateTime.UtcNow.ToLongTimeString()}");
            try
            {
                _context.Entry(VATDetails).State = EntityState.Modified;
                _context.VATPercentages_History.Add(_recorder.VATPercentageHistoryCreator(VATDetails, "Update Record", User));
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error occurred while UpdateVATPriceAsync(), Exception : {1}", ex.ToString());
                return false;
            }
        }
        public async Task<bool> DeleteVATPriceAsync(int Id, string User)
        {
            _logger.LogInformation($"[VATRepo]DeleteVATPriceAsync(int id) hit at {DateTime.UtcNow.ToLongTimeString()}");
            try
            {
                var vatRecord = await _context.VATPercentages.FindAsync(Id);
                if (vatRecord == null)
                {
                    _logger.LogInformation($"[VATRepo]DeleteVATPriceAsync(Id) [VAT RECORD NOT FOUND] hit at {DateTime.UtcNow.ToLongTimeString()}");
                    return false;
                }
                else
                {
                    _context.VATPercentages_History.Add(_recorder.VATPercentageHistoryCreator(vatRecord, "Delete Record", User));
                    _context.VATPercentages.Remove(vatRecord);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation($"[VATRepo]DeleteVATPriceAsync(Id) [VAT RECORD DELETED SUCCESSFULLY] hit at {DateTime.UtcNow.ToLongTimeString()}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error occurred while DeleteVATPriceAsync(int Id), Exception : {1}", ex.ToString());
                return false;
            }
        }
        public async Task<List<RetailStoreVATPercentage>> GetVATPricesListAsync()
        {
            _logger.LogInformation($"[VATRepo]GetVATPricesListAsync() hit at {DateTime.UtcNow.ToLongTimeString()}");
            try
            {
                return await _context.VATPercentages.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error occurred while GetVATPricesListAsync(), Exception : {1}", ex.ToString());
                return null;
            }
        }
        public async Task<RetailStoreVATPercentage> FindVATPriceDetailsAsync(int Id)
        {
            _logger.LogInformation($"[VATRepo]FindVATPriceDetailsAsync(int id) hit at {DateTime.UtcNow.ToLongTimeString()}");
            try
            {
                return await _context.VATPercentages.FindAsync(Id);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error occurred while FindVATPriceDetailsAsync(int Id), Exception : {1}", ex.ToString());
                return null;
            }
        }
    }
}
