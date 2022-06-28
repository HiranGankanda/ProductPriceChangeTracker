using PPCT.DataSupport.DataModels.ProjectTableModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPCT.RepositoryServices.VATRepoServices
{
    public class VATService : IVATService
    {
        private readonly IVATRepo _Repo;
        public VATService(IVATRepo Repo)
        {
            _Repo = Repo;
        }

        public async Task<bool> CreateNewVATPriceServiceAsync(RetailStoreVATPercentage VATDetails, string User)
        {
            return await _Repo.CreateNewVATPriceAsync(VATDetails, User);
        }
        public Task<bool> UpdateVATPriceServiceAsync(RetailStoreVATPercentage VATDetails, string User) => _Repo.UpdateVATPriceAsync(VATDetails, User);
        public Task<bool> DeleteVATPriceServiceAsync(int Id, string User) => _Repo.DeleteVATPriceAsync(Id, User);
        public Task<List<RetailStoreVATPercentage>> GetVATPricesListServiceAsync() => _Repo.GetVATPricesListAsync();
        public Task<RetailStoreVATPercentage> FindVATPriceDetailsServiceAsync(int Id) => _Repo.FindVATPriceDetailsAsync(Id);
    }
}