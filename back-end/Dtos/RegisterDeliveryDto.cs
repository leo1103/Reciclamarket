using System;
using basurapp.api.Models;

namespace basurapp.api.Dtos
{
    public class RegisterDeliveryDto
    {
        public string title { get; set; }
        public string description { get; set; }
        public int idBasurappUserContributor { get; set; }
        public decimal altitude { get; set; }
        public decimal latitude { get; set; }
        public DateTime date { get; set; }
    }
}