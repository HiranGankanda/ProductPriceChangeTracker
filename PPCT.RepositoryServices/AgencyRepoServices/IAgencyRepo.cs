using PPCT.DataAccessLayer.DataModels.ProjectTableModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPCT.RepositoryServices.AgencyRepoServices
{
    public interface IAgencyRepo
    {
        Task<bool> CreateNewAgency(Agency AgencyDetails);
        Task<bool> UpdateAgency(Agency AgencyDetails);
        Task<bool> DeleteAgency(int AgencyId);
        Task<List<Agency>> GetAgenciesListAsync();
        Task<Agency> FindAgencyDetails(int Id);
    }
}