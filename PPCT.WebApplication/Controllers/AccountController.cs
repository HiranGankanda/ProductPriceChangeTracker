using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PPCT.DataSupport;
using PPCT.WebApplication.APIAccess;
using PPCT.WebApplication.Helpers;
using System;
using System.Text;
using System.Threading.Tasks;

namespace PPCT.WebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly ReadTokenData _tkReader;
        private readonly IUserAccounts _accrepo;
        public AccountController(IUserAccounts accrepo, ReadTokenData tkReader)
        {
            _accrepo = accrepo;
            _tkReader = tkReader;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            try
            {
                var tokenData = await _accrepo.LoginAsync(model.Username, model.Password);
                if(tokenData.Message == "Success")
                {
                    Response.Cookies.Append("X-Access-Token", tokenData.AccessToken, new CookieOptions { 
                        HttpOnly = true,
                        SameSite = SameSiteMode.Strict
                    });
                    Response.Cookies.Append("X-Refresh-Token", tokenData.RefreshToken, new CookieOptions
                    {
                        HttpOnly = true,
                        SameSite = SameSiteMode.Strict
                    });
                    Response.Cookies.Append("FullName", await CipherText.EncriptAsync(_tkReader.CurrentUserFullNameFromToken(tokenData.AccessToken)), new CookieOptions { 
                        IsEssential = true,
                        HttpOnly = true,
                        SameSite = SameSiteMode.Strict
                    });
                    Response.Cookies.Append("Username", await CipherText.EncriptAsync(_tkReader.CurrentUserNameFromToken(tokenData.AccessToken)), new CookieOptions
                    {
                        IsEssential = true,
                        HttpOnly = true,
                        SameSite = SameSiteMode.Strict
                    });
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
