using Microsoft.AspNetCore.Mvc;
using StockTrackingMVC.Data;
using StockTrackingMVC.Models;

namespace StockTrackingMVC.Controllers
{
	public class CheckingController : Controller
	{
		private readonly StockTrackingDBContext _dbContext;
		public CheckingController(StockTrackingDBContext dBContext)
		{
			_dbContext = dBContext;
		}
		public IActionResult Index()
		{
			return View();
		}

		public void GetUserLoggedIn()
		{
			var loggedUsers = _dbContext.Users.Where(u => u.KeepLoggedIn);
		}
		
	}
}
