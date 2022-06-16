using PPCT.DataAccessLayer.DataModels.ProjectTableModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPCT.RepositoryServices.RetailStoreRepoServices
{
    public class RetailStoreServices : IRetailStoreServices
    {
        private readonly IRetailStoreRepo _comRepo;
        public RetailStoreServices(IRetailStoreRepo comRepo)
        {
            _comRepo = comRepo;
        }

        public Task<bool> CreateNewRetailStoreServiceAsync(RetailStore CompanyDetails) => _comRepo.CreateNewRetailStoreAsync(CompanyDetails);
        public Task<bool> UpdateRetailStoreServiceAsync(RetailStore CompanyDetails) => _comRepo.UpdateRetailStoreAsync(CompanyDetails);
        public Task<bool> DeleteRetailStoreServiceAsync(int CompanyId) => _comRepo.DeleteRetailStoreAsync(CompanyId);
        public Task<List<RetailStore>> GetRetailStoresListServiceAsync() => _comRepo.GetRetailStoresListAsync();
        public Task<RetailStore> FindRetailStoreDetailsServiceAsync(int CompanyId) => _comRepo.FindRetailStoreDetailsAsync(CompanyId);
    }
}