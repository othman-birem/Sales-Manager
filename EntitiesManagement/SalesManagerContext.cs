using Microsoft.EntityFrameworkCore;

namespace Sales_Manager.EntitiesManagement
{
    public class SalesManagerContext : DbContext
    {
        public SalesManagerContext(DbContextOptions<SalesManagerContext> options) : base(options) { }

        public DbSet<OrderBase> Orders { get; set; }
        public DbSet<UserBase> Users { get; set; }
        public DbSet<ProductBase> Products { get; set; }
        public DbSet<CustomerBase> Customers { get; set; }
        public DbSet<ItemBase> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=OTHMAN-DESKTOP\\MYFIRSTINSTACE;Database=SalesManagerDB;Integrated Security=True;TrustServerCertificate=True;");
            }
        }
    }
}
