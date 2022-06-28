using Flurl;
using Flurl.Http;
using PPCT.DataSupport;
using PPCT.DataSupport.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPCT.WebApplication.APIAccess
{
    public class UserAccounts : IUserAccounts
    {
        public async Task<TokenModel> LoginAsync(string Username, string Password)
        {
            return await "http://localhost:30812"
                .AppendPathSegment("/api/Authenticate/login")
                .PostJsonAsync(new Login
                {
                    Username = Username,
                    Password = Password
                })
                .ReceiveJson<TokenModel>();
        }
    }
}
