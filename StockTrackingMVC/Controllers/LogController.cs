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
				objLogList = objLogList.Where(p => p.LogUser.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
			}
			int totalLogs = objLogList.Count();

			int totalPages = (int)Math.Ceiling((double)totalLogs / pageSize);

			page = Math.Max(1, Math.Min(page, totalPages));

			int skipCount = (page - 1) * pageSize;

			var logs = objLogList
				.OrderByDescending(log => log.LogTime)
				.Skip(skipCount)
				.Take(pageSize)
				.ToList();

			ViewBag.Logs = logs;
			ViewBag.CurrentPage = page;
			ViewBag.TotalPages = totalPages;
			return View();

		}
	}
}
