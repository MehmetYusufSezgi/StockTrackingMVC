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
        [Range(0, int.MaxValue, ErrorMessage = "Ürün miktarı 0'dan küçük olamaz.")]
        public int ProductAmount { get; set; }
        [DisplayName("Alış Fiyatı")]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Alış Fiyatı 0'dan küçük olamaz.")]
        public int ProductBuyingPrice { get; set; }
        [DisplayName("Satış Fiyatı")]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Satış Fiyatı 0'dan küçük olamaz.")]
        public int ProductSellingPrice { get; set; }
        public Category? Category { get; set; }
    }
}
