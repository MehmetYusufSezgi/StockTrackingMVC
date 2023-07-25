using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StockTrackingMVC.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }
		[Required]
		[DisplayName("Kullanıcı Şifresi")]
		public string UserPassword { get; set; }
		[Required]
		[DisplayName("Kullanıcı Türü")]
		public string UserType { get; set; }
        public bool KeepLoggedIn { get; set; }
    }
}
