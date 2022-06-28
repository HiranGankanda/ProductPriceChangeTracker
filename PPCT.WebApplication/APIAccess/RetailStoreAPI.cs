using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.AspNetCore.Http;
using PPCT.DataSupport.DataModels.ProjectTableModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPCT.WebApplication.APIAccess
{
    public class RetailStoreAPI : RepositoryAPIBase, IRetailStoreAPI
    {
        public RetailStoreAPI(IFlurlClientFactory flurlClientFactory, IHttpContextAccessor httpContextAccessor)
            :base(flurlClientFactory, httpContextAccessor)
        {
        }

        public async Task<List<RetailStore>> APIGetRetailStoresListAsync()
        {
            try
            {
                return await _flurlClient.Request("/api/RetailStores/GetAllRetailStores").GetJsonAsync<List<RetailStore>>();                
            }
            catch (FlurlHttpException ex)
            {
                if (ex.StatusCode == 401)
                    await GetNewAccessTokenAsync();
                return null;
            }
        }
    }
}
