using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockTrackingMVC.Data;
using StockTrackingMVC.Models;

namespace StockTrackingMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly StockTrackingDBContext _dbcontext;
        public ProductController(StockTrackingDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> objProductList = _dbcontext.Products.Include(p => p.Category);
            return View(objProductList);
        }
        //GET
        public IActionResult Add()
        {
			ViewBag.Categories = _dbcontext.Categories.ToList();
			return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Product obj)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Products.Add(obj);
                var message = User.Identity.Name + " ürün ekledi : " + obj.ProductName;
                var logEntry = new Log
                {
                    LogUser = User.Identity.Name,
                    LogMessage = message,
                    LogTime = DateTime.Now
                };
                _dbcontext.Logs.Add(logEntry);
				TempData["success"] = "Ürün başarıyla eklendi";
				_dbcontext.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.Categories = _dbcontext.Categories.ToList();
			return View(obj);
        }

		//GET
		public IActionResult Edit(int? id)
		{
            if(id==null || id == 0)
            {
                return NotFound();
            }
            var productFromDb = _dbcontext.Products.Find(id);
            if (productFromDb == null)
            {
                return NotFound();
            }
			ViewBag.Categories = _dbcontext.Categories.ToList();
			return View(productFromDb);
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Product obj)
		{
			if (ModelState.IsValid)
			{
				_dbcontext.Products.Update(obj);
				var message = User.Identity.Name + " ürün güncelledi : " + obj.ProductName;
				var logEntry = new Log
				{
					LogUser = User.Identity.Name,
					LogMessage = message,
					LogTime = DateTime.Now
				};
				_dbcontext.Logs.Add(logEntry);
				_dbcontext.SaveChanges();
				TempData["success"] = "Ürün başarıyla güncellendi";
				return RedirectToAction("Index");
			}
            var value=  ModelState.Values.SelectMany(v => v.Errors);
			// If the ModelState is not valid, return the Edit view with the model data
			ViewBag.Categories = _dbcontext.Categories.ToList();
			return View("Edit", obj);
		}


		//GET
		public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var productFromDb = _dbcontext.Products.Find(id);
            if (productFromDb == null)
            {
                return NotFound();
            }
			ViewBag.Categories = _dbcontext.Categories.ToList();
			return View(productFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var productFromDb = _dbcontext.Products.Find(id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            _dbcontext.Products.Remove(productFromDb);
			var message = User.Identity.Name + " ürün sildi : " + productFromDb.ProductName;
			var logEntry = new Log
			{
				LogUser = User.Identity.Name,
				LogMessage = message,
				LogTime = DateTime.Now
			};
			_dbcontext.Logs.Add(logEntry);
			_dbcontext.SaveChanges();
			ViewBag.Categories = _dbcontext.Categories.ToList();
			TempData["success"] = "Ürün başarıyla silindi";
			return RedirectToAction("Index");
        }

		public IActionResult Sell(int id)
		{
			var productFromDb = _dbcontext.Products.Find(id);
			if (productFromDb == null)
			{
				return NotFound();
			}

			// Reduce the product amount by one if it's greater than zero
			if (productFromDb.ProductAmount > 0)
			{
				productFromDb.ProductAmount--;
				var message = User.Identity.Name + " ürün sattı : " + productFromDb.ProductName;
				var logEntry = new Log
				{
					LogUser = User.Identity.Name,
					LogMessage = message,
					LogTime = DateTime.Now
				};
				_dbcontext.Logs.Add(logEntry);
				_dbcontext.SaveChanges();
			}
            TempData["success"] = "Ürün başarıyla satıldı";
            return RedirectToAction("Index");
		}
	}
}
