using Sales_Manager.EntitiesManagement;

namespace Sales_Manager.Services
{
    public class CustomerService
    {
        private SalesManagerContext _context;

        internal CustomerService(DesignTimeDbContextFactory factory)
        {
            _context = factory.CreateDbContext(null);
        }
    }
}
