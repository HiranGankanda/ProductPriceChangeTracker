using PPCT.DataSupport.DataModels.ProjectTableModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPCT.RepositoryServices.VATRepoServices
{
    public interface IVATRepo
    {
        Task<bool> CreateNewVATPriceAsync(RetailStoreVATPercentage VATDetails, string User);
        Task<bool> UpdateVATPriceAsync(RetailStoreVATPercentage VATDetails, string User);
        Task<bool> DeleteVATPriceAsync(int Id, string User);
        Task<List<RetailStoreVATPercentage>> GetVATPricesListAsync();
        Task<RetailStoreVATPercentage> FindVATPriceDetailsAsync(int Id);
    }
}
