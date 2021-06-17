using basurapp.api.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace basurapp.api.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base (options){}
        public DbSet<BasurappUser> BasurappUser { get; set; }        
        public DbSet<Delivery> Delivery  { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}