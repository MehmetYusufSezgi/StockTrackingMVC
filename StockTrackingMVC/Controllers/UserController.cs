using Microsoft.AspNetCore.Mvc;
using StockTrackingMVC.Data;
using StockTrackingMVC.Models;

namespace StockTrackingMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly StockTrackingDBContext _dbcontext;
        public UserController(StockTrackingDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public IActionResult Index()
        {
            IEnumerable<User> objUserList = _dbcontext.Users;
            return View(objUserList);
        }

		//GET
		public IActionResult Add()
		{
			ViewBag.Users = _dbcontext.Users.ToList();
			return View();
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Add(User obj)
		{
			if (ModelState.IsValid)
			{
				_dbcontext.Users.Add(obj);
				_dbcontext.SaveChanges();
				TempData["success"] = "Ürün başarıyla eklendi";
				return RedirectToAction("Index");
			}
			ViewBag.Users = _dbcontext.Users.ToList();
			return View(obj);
		}
	}
}
