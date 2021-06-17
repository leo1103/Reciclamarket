namespace basurapp.api.Models
{
    public class BasurappUser
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string realName { get; set; }
        public string lastName { get; set; }
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
        public string phone { get; set; }
        public int role { get; set; }
    }
}