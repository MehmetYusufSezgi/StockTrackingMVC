using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using StockTrackingMVC.Models;
using System.Threading.Tasks;
using StockTrackingMVC.Data;
using Microsoft.EntityFrameworkCore;

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
			User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == modelLogin.UserName);

			if (user != null && user.UserPassword == modelLogin.UserPassword)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.UserName),
					new Claim(ClaimTypes.Name, modelLogin.UserName),
					new Claim(ClaimTypes.Role, user.UserType),
					new Claim("OtherProperties", "Example Role")
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

				if (modelLogin.KeepLoggedIn)
				{
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties
					{
						IsPersistent = true,
						ExpiresUtc = DateTime.UtcNow.AddMonths(1) // Beni hatırla için 1 ay hatırlama
					});
				}
				else
				{
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
					// ExpiresUtc belirlenmediği için Cookie otomatik oluşturulacak
					// Cookie siteden çıkılınca silinir
				}

				return RedirectToAction("Index", "Home");
            }
            ViewData["ValidateMessage"] = "Kullanıcı Bulunamadı";
            return View();
        }
    }
}
