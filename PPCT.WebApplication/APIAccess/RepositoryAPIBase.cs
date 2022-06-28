using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.AspNetCore.Http;
using PPCT.DataSupport.CustomModels;
using System.Threading.Tasks;

namespace PPCT.WebApplication.APIAccess
{
    //https://www.youtube.com/watch?v=2ZQzMB5YA_U&t=2753s&ab_channel=NobleCauseSoftwareDevelopment
    public class RepositoryAPIBase
    {
        protected readonly IFlurlClient _flurlClient;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public RepositoryAPIBase(IFlurlClientFactory flurlClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _flurlClient = flurlClientFactory.Get("http://localhost:30812");
            _httpContextAccessor = httpContextAccessor;
                       

            _flurlClient.BeforeCall(flurlCall => {
                var accessToken = _httpContextAccessor.HttpContext.Request.Cookies["X-Access-Token"];
                if(!string.IsNullOrWhiteSpace(accessToken))
                {
                    flurlCall.HttpRequestMessage.SetHeader("Authorization", $"bearer {accessToken}");
                }
                else
                {
                    flurlCall.HttpRequestMessage.SetHeader("Authorization", string.Empty);
                }
            });
        }

        public async Task GetNewAccessTokenAsync()
        {
            TokenModel Result = await _flurlClient
                    .Request("/api/Authenticate/refresh-token")
                    .PostJsonAsync(new TokenModel
                    {
                        AccessToken = _httpContextAccessor.HttpContext.Request.Cookies["X-Access-Token"],
                        RefreshToken = _httpContextAccessor.HttpContext.Request.Cookies["X-Refresh-Token"]
                    })
                    .ReceiveJson<TokenModel>();

            if (Result.Message == "Success")
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("X-Access-Token");
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("X-Refresh-Token");

                _httpContextAccessor.HttpContext.Response.Cookies.Append("X-Access-Token", Result.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                });
                _httpContextAccessor.HttpContext.Response.Cookies.Append("X-Refresh-Token", Result.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                });
            }
        }

        //public void IfEmptyOrInvalidToken(string token)
        //{
        //    if (!string.IsNullOrEmpty(token))
        //    {
        //        var jwtToken = new JwtSecurityToken(token);
        //        long CurrentUnixSeconds = DateTimeOffset.Now.ToUnixTimeSeconds();
        //        long TokenExpTime = jwtToken.Payload.Exp ?? 0;

        //        if (TokenExpTime < CurrentUnixSeconds)
        //        {
        //            Task.Run(async () => await GetNewAccessTokenAsync());
        //        }
        //    }
        //}
    }
}