using PPCT.DataAccessLayer.DataModels.ProjectTableModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPCT.RepositoryServices.AgencyRepoServices
{
    public interface IAgencyService
    {
        Task<bool> CreateNewAgencyServiceAsync(Agency AgencyDetails);
        Task<bool> UpdateAgencyServiceAsync(Agency AgencyDetails);
        Task<bool> DeleteAgencyServiceAsync(int Id);
        Task<List<Agency>> GetAgenciesListServiceAsync();
        Task<Agency> FindAgencyDetailsServiceAsync(int Id);
    }
}
