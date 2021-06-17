using System;

namespace basurapp.api.Dtos
{
    public class DeliveryForGet
    {
        public UserForGet basurappUserContributor { get; set; }
        public UserForGet basurappUserRecolector { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public decimal altitude { get; set; }
        public decimal latitude { get; set; }
        public DateTime date { get; set; }
        public int deliveryState { get; set; }        
    }
}