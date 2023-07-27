using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StockTrackingMVC.Models
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("Kullanıcı Şifresi")]
        public string UserPassword { get; set; }

        public bool KeepLoggedIn { get; set; }
    }
}
