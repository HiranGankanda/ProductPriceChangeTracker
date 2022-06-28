using PPCT.DataSupport.DataModels.ProjectTableModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPCT.RepositoryServices.VATRepoServices
{
    public interface IVATService
    {
        Task<bool> CreateNewVATPriceServiceAsync(RetailStoreVATPercentage VATDetails, string User);
        Task<bool> UpdateVATPriceServiceAsync(RetailStoreVATPercentage VATDetails, string User);
        Task<bool> DeleteVATPriceServiceAsync(int Id, string User);
        Task<List<RetailStoreVATPercentage>> GetVATPricesListServiceAsync();
        Task<RetailStoreVATPercentage> FindVATPriceDetailsServiceAsync(int Id);
    }
}
