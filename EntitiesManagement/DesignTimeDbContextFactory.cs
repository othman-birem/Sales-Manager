using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sales_Manager.EntitiesManagement
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SalesManagerContext>
    {
        public SalesManagerContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SalesManagerContext>();
            optionsBuilder.UseSqlServer("Server=OTHMAN-DESKTOP\\MYFIRSTINSTACE;Database=SalesManagerDB;Integrated Security=True;TrustServerCertificate=True;");

            return new SalesManagerContext(optionsBuilder.Options);
        }
    }
}
