using System;

namespace basurapp.api.Models
{
    public class Delivery
    {
        public int id { get; set; }
        public BasurappUser basurappUserContributor { get; set; }
        public BasurappUser basurappUserRecolector { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        public decimal altitude { get; set; }
        public decimal latitude { get; set; }
        public int deliveryState { get; set; }
    }
}