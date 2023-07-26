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
				// Get the selected UserType value from the dropdown
				string selectedUserType = Request.Form["UserType"];

				// Update the UserType property of the object with the selected value
				obj.UserType = selectedUserType;

				_dbcontext.Users.Add(obj);
				_dbcontext.SaveChanges();
				TempData["success"] = "Kullanıcı başarıyla eklendi";
				return RedirectToAction("Index");
			}

			ViewBag.Users = _dbcontext.Users.ToList();
			return View(obj);
		}


		//GET
		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var userFromDb = _dbcontext.Users.Find(id);
			if (userFromDb == null)
			{
				return NotFound();
			}
			ViewBag.Users = _dbcontext.Users.ToList();
			return View(userFromDb);
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(User obj)
		{
			if (ModelState.IsValid)
			{
				// Get the selected UserType value from the dropdown
				string selectedUserType = Request.Form["UserType"];

				// Update the UserType property of the object with the selected value
				obj.UserType = selectedUserType;

				_dbcontext.Users.Update(obj);
				_dbcontext.SaveChanges();
				TempData["success"] = "Kullanıcı başarıyla güncellendi";
				return RedirectToAction("Index");
			}

			ViewBag.Users = _dbcontext.Users.ToList();
			return View(obj);
		}

		//GET
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var userFromDb = _dbcontext.Users.Find(id);
			if (userFromDb == null)
			{
				return NotFound();
			}
			ViewBag.Users = _dbcontext.Users.ToList();
			return View(userFromDb);
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePOST(int? id)
		{
			var userFromDb = _dbcontext.Users.Find(id);
			if (userFromDb == null)
			{
				return NotFound();
			}
			_dbcontext.Users.Remove(userFromDb);
			_dbcontext.SaveChanges();
			ViewBag.Users = _dbcontext.Users.ToList();
			TempData["success"] = "Kullanıcı başarıyla silindi";
			return RedirectToAction("Index");
		}
	}
}
