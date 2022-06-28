using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PPCT.WebApplication.Helpers
{
    public class ReadTokenData
    {
        #region Constructor
        private readonly string AppKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("JWT")["Secret"];
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        private JwtSecurityToken jsonToken;
        public ReadTokenData(JwtSecurityTokenHandler tokenHandler, IHttpContextAccessor IHttpContextAccessor)
        {
            _tokenHandler = tokenHandler;
            _IHttpContextAccessor = IHttpContextAccessor;
        }
        #endregion

        #region MainMethodes
        public bool CheckIsEmptyOrInvalidToken(string token)
        {
            if (!string.IsNullOrEmpty(token))
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
        public string CurrentUserFullNameFromToken(string token)
        {
            jsonToken = _tokenHandler.ReadToken(GetTokenFromCookiesOrMethode(token)) as JwtSecurityToken;
            return jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname").Value;
        }
        public string CurrentUserFullNameFromCookie()
        {
            return CipherText.Decrypt(_IHttpContextAccessor.HttpContext.Request.Cookies["FullName"]?.ToString());
        }
        public string CurrentUserNameFromToken(string token)
        {
            jsonToken = _tokenHandler.ReadToken(GetTokenFromCookiesOrMethode(token)) as JwtSecurityToken;
            return jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
        }
        public string CurrentUserNameFromCookie()
        {
            return CipherText.Decrypt(_IHttpContextAccessor.HttpContext.Request.Cookies["Username"]?.ToString());
        }
        public string CurrentUserRoles()
        {
            try
            {
                List<string> Roles = new List<string>();
                jsonToken = _tokenHandler.ReadToken(GetTokenFromCookiesOrMethode(null)) as JwtSecurityToken;

                foreach (Claim claim in jsonToken.Claims)
                {
                    if (claim.Type == ClaimTypes.Role)
                    {
                        Roles.Add(claim.Value);
                    }
                }
                return string.Join(" | ", Roles);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool test()
        {
            return true;
        }
        public async Task<bool> IsCurrentUserInRoleAsync(string role)
        {
            return await Task.Run(() => IsCurrentUserInRole(role));
        }
        public bool IsCurrentUserInRole(string role)
        {
            try
            {
                bool rslt = false;
                string token = GetTokenFromCookiesOrMethode(null);
                jsonToken = _tokenHandler.ReadToken(token) as JwtSecurityToken;
                foreach (Claim claim in jsonToken.Claims)
                {
                    if (claim.Type == ClaimTypes.Role)
                    {
                        if (claim.Value == role)
                        {
                            rslt = true;
                            break;
                        }
                    }
                }
                return rslt;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region SubMethodes
        private TokenValidationParameters GetTokenValidationParam()
        {
            return new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppKey)),
                ValidateLifetime = false
            };
        }
        private string GetTokenFromCookiesOrMethode(string token)
        {
            if (string.IsNullOrEmpty(token))
                return _IHttpContextAccessor.HttpContext.Request.Cookies["X-Access-Token"]?.ToString();
            else
                return token;
        }
        #endregion
    }
}