using Microsoft.EntityFrameworkCore;
using StockTrackingMVC.Models;

namespace StockTrackingMVC.Data
{
    public class StockTrackingDBContext:DbContext
    {
        public StockTrackingDBContext(DbContextOptions<StockTrackingDBContext> options) : base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
