using Microsoft.EntityFrameworkCore;
using Sales_Manager.Models;

namespace Sales_Manager.EntitiesManagement
{
    public class SalesManagerContext : DbContext
    {
        public SalesManagerContext(DbContextOptions<SalesManagerContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=OTHMAN-DESKTOP\\MYFIRSTINSTACE;Database=SalesManagerDB;Integrated Security=True;TrustServerCertificate=True;");
            }
        }
    }
}
