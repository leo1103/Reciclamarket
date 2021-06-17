using System.Collections.Generic;
using basurapp.api.Models;
using Newtonsoft.Json;
using System.Linq;

namespace basurapp.api.Data
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            dataContext = context;
        }

        public void seedUsers()
        {
            if(!dataContext.BasurappUser.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<BasurappUser>>(userData);
                foreach (var user in users)
                {
                    byte[] passwordHash, passwordSalt;
                    createPasswordHash("password", out passwordHash, out passwordSalt);

                    user.passwordHash = passwordHash;
                    user.passwordSalt = passwordSalt;
                    user.userName = user.userName.ToLower();

                    dataContext.BasurappUser.Add(user);
                }

                dataContext.SaveChanges();
            }
        }

        public void seedProducts()
        {
            if(!dataContext.Product.Any())
            {
                var productData = System.IO.File.ReadAllText("Data/ProductSeedData.json");
                var products = JsonConvert.DeserializeObject<List<Product>>(productData);
                foreach (var product in products)
                {
                    dataContext.Product.Add(product);
                }
                dataContext.SaveChanges();
            }
        }        

        private void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hMac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hMac.Key;
                passwordHash = hMac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

    }
}