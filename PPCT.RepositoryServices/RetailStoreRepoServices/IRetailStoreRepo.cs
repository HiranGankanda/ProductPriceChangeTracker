using PPCT.DataSupport.DataModels.ProjectTableModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPCT.RepositoryServices.RetailStoreRepoServices
{
    public interface IRetailStoreRepo
    {
        Task<bool> CreateNewRetailStoreAsync(RetailStore RetailStoreDetails);
        Task<bool> UpdateRetailStoreAsync(RetailStore RetailStoreDetails);
        Task<bool> DeleteRetailStoreAsync(int Id);
        Task<List<RetailStore>> GetRetailStoresListAsync();
        Task<RetailStore> FindRetailStoreDetailsAsync(int Id);
    }
}
