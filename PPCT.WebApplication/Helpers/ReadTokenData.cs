using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace PPCT.WebApplication.Helpers
{
    public class ReadTokenData
    {
        public bool CheckIsEmptyOrInvalidToken(string token)
        {
            if(!string.IsNullOrEmpty(token))
            {
                return true;
            }
            
            var jwtToken = new JwtSecurityToken(token);
            long CurrentUnixSeconds = DateTimeOffset.Now.ToUnixTimeSeconds();
            long TokenExpTime = jwtToken.Payload.Exp ?? 0;

            if (TokenExpTime < CurrentUnixSeconds)
                return true;
            else
                return false;
        }
        public string CurrentUserName(string token)
        {
            return "";
        }
    }
}
