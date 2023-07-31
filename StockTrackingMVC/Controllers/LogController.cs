using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockTrackingMVC.Data;
using StockTrackingMVC.Models;
using System.Drawing.Printing;

namespace StockTrackingMVC.Controllers
{
    [Authorize]
    public class LogController : Controller
    {
        private readonly StockTrackingDBContext _dbcontext;
        public LogController(StockTrackingDBContext dBContext)
        {
            _dbcontext = dBContext;
        }
        public IActionResult Index(string searchQuery, int page = 1, int pageSize = 10)
        {
            IEnumerable<Log> objLogList = _dbcontext.Logs;
            if (!string.IsNullOrEmpty(searchQuery))
            {
                // Perform the filtering based on the search query, for example:
                objLogList = objLogList.Where(p => p.LogUser.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
            }
            // Get the total number of logs in the database
            int totalLogs = objLogList.Count();

            // Calculate the number of pages based on the pageSize
            int totalPages = (int)Math.Ceiling((double)totalLogs / pageSize);

            // Ensure the page number is within the valid range
            page = Math.Max(1, Math.Min(page, totalPages));

            // Calculate the skip count based on the page number and pageSize
            int skipCount = (page - 1) * pageSize;

            // Retrieve the logs for the current page using Skip() and Take() methods
            var logs = objLogList
                .OrderByDescending(log => log.LogTime)
                .Skip(skipCount)
                .Take(pageSize)
                .ToList();

            // Pass the logs and pagination information to the view
            ViewBag.Logs = logs;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            return View();

        }
    }
}
