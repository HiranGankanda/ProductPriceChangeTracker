using Microsoft.AspNetCore.Mvc;
using PPCT.WebApplication.APIAccess;
using PPCT.WebApplication.Helpers;
using PPCT.WebApplication.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PPCT.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        #region Constructor
        private readonly IRetailStoreAPI _Repo;
        private readonly ReadTokenData _tkReader;
        public HomeController(IRetailStoreAPI Repo, ReadTokenData tkReader)
        {
            _Repo = Repo;
            _tkReader = tkReader;
        }
        #endregion

        #region Controllers
        public IActionResult Index()
        {
            ViewBag.UserRoles = _tkReader.CurrentUserRoles();
            ViewBag.UserFullName = _tkReader.CurrentUserFullNameFromCookie();
            return View();
        }
        public async Task<IActionResult> ManageRetailStores()
        {
            ViewBag.RetailStoreData = await _Repo.APIGetRetailStoresListAsync();
            return View();
        }
        #endregion

        #region Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}