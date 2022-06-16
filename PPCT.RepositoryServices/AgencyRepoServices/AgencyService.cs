using PPCT.DataAccessLayer.DataModels.ProjectTableModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPCT.RepositoryServices.AgencyRepoServices
{
    public class AgencyService : IAgencyService
    {
        private readonly IAgencyRepo _Repo;
        public AgencyService(IAgencyRepo Repo)
        {
            _Repo = Repo;
        }

        public async Task<bool> CreateNewAgencyServiceAsync(Agency AgencyDetails) => await _Repo.CreateNewAgency(AgencyDetails);
        public async Task<bool> UpdateAgencyServiceAsync(Agency AgencyDetails) => await _Repo.UpdateAgency(AgencyDetails);
        public async Task<bool> DeleteAgencyServiceAsync(int Id) => await _Repo.DeleteAgency(Id);
        public async Task<List<Agency>> GetAgenciesListServiceAsync() => await _Repo.GetAgenciesListAsync();
        public async Task<Agency> FindAgencyDetailsServiceAsync(int Id) => await _Repo.FindAgencyDetails(Id);
    }
}