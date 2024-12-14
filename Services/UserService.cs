using Sales_Manager.EntitiesManagement;

namespace Sales_Manager.Services
{
    public class UserService
    {
        private SalesManagerContext _context;
        internal UserService(DesignTimeDbContextFactory factory)
        {
            _context = factory.CreateDbContext(null);
        }
    }
}
