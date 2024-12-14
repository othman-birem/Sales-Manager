using Sales_Manager.EntitiesManagement;

namespace Sales_Manager.Services
{
    public class OrderService
    {
        private SalesManagerContext _context;

        internal OrderService(DesignTimeDbContextFactory factory)
        {
            _context = factory.CreateDbContext(null);
        }
    }
}
