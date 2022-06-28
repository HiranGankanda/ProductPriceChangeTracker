using PPCT.DataSupport.DataModels.ProjectTableModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPCT.WebApplication.APIAccess
{
    public interface IRetailStoreAPI
    {
        Task<List<RetailStore>> APIGetRetailStoresListAsync();
    }
}
