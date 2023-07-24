namespace StockTrackingMVC.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserType { get; set; }
        public bool KeepLoggedIn { get; set; }
    }
}
