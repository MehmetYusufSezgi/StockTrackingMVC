using Microsoft.AspNetCore.Mvc;
using StockTrackingMVC.Data;

namespace StockTrackingMVC.Controllers
{
    public class CheckingController : Controller
    {
        private readonly StockTrackingDBContext _dBContext;

        public CheckingController(StockTrackingDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
