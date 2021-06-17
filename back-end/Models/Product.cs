namespace basurapp.api.Models
{
    public class Product
    {
        public int id { get; set; }
        public BasurappUser creator{ get; set; }
        public string productName { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public decimal price { get; set; }
    }
}