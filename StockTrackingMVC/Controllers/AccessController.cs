using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using StockTrackingMVC.Models;
using System.Threading.Tasks;
using StockTrackingMVC.Data;

namespace StockTrackingMVC.Controllers
{
    public class AccessController : Controller
    {
        private readonly StockTrackingDBContext _dbContext;
        public AccessController(StockTrackingDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User modelLogin)
        {
            if(modelLogin.UserName == "Mehmet Yusuf SEZGİ" && modelLogin.UserPassword == "4412")
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.UserName),
                    new Claim("OtherProperties", "Example Role")
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties() {
                    AllowRefresh = true,
                    IsPersistent = modelLogin.KeepLoggedIn
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                return RedirectToAction("Index", "Home");
            }
            ViewData["ValidateMessage"] = "Kullanıcı Bulunamadı";
            return View();
        }
    }
}
