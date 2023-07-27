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
        private readonly StockTrackingDBContext _dbcontext;

        public AccessController(StockTrackingDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        public User GetUserByCredentials(string username, string password)
        {
            return _dbcontext.Users.SingleOrDefault(u => u.UserName == username && u.UserPassword == password);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                User foundUser = GetUserByCredentials(loginViewModel.UserName, loginViewModel.UserPassword);

                if (foundUser != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, foundUser.UserName),
                    };

                    if (!string.IsNullOrEmpty(foundUser.UserType))
                    {
                        claims.Add(new Claim(ClaimTypes.Role, foundUser.UserType));
                    }

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity));

                    return RedirectToAction("Index", "Home");
                }

                ViewData["ValidateMessage"] = "Invalid username or password.";
            }
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
