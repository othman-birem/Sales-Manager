using Sales_Manager.EntitiesManagement;

namespace Sales_Manager.Services
{
    public class ItemService
    {
        private SalesManagerContext _context;

        internal ItemService(DesignTimeDbContextFactory factory)
        {
            _context = factory.CreateDbContext(null);
        }
    }
}
