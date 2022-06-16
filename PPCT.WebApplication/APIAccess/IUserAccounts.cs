using PPCT.DataAccessLayer.CustomModels;
using System.Threading.Tasks;

namespace PPCT.WebApplication.APIAccess
{
    public interface IUserAccounts
    {
        Task<TokenModel> LoginAsync(string Username, string Password);
    }
}
