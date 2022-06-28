using PPCT.DataSupport.DataModels.ProjectTableModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPCT.RepositoryServices.RetailStoreRepoServices
{
    public interface IRetailStoreServices
    {
        Task<bool> CreateNewRetailStoreServiceAsync(RetailStore RetailStoreDetails);
        Task<bool> UpdateRetailStoreServiceAsync(RetailStore RetailStoreDetails);
        Task<bool> DeleteRetailStoreServiceAsync(int RetailStoreId);
        Task<List<RetailStore>> GetRetailStoresListServiceAsync();
        Task<RetailStore> FindRetailStoreDetailsServiceAsync(int RetailStoreId);
    }
}
