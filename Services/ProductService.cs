using Sales_Manager.EntitiesManagement;

namespace Sales_Manager.Services
{
    public class ProductService
    {
        private SalesManagerContext _context;

        internal ProductService(DesignTimeDbContextFactory factory)
        {
            _context = factory.CreateDbContext(null);
        }
    }
}
