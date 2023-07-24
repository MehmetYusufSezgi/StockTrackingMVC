using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StockTrackingMVC.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Ürün Adı")]
        [Required]
        public string ProductName { get; set; }
        [DisplayName("Stok Kodu")]
        public string StockCode { get; set; }
        [DisplayName("Kategori")]
        [Required]
        public int CategoryId { get; set; }
        [DisplayName("Ürün Miktarı")]
        [Required]
        public int ProductAmount { get; set; }
        [DisplayName("Alış Fiyatı")]
        [Required]
        public int ProductBuyingPrice { get; set; }
        [DisplayName("Satış Fiyatı")]
        [Required]
        public int ProductSellingPrice { get; set; }
        public Category Category { get; set; }
    }
}
