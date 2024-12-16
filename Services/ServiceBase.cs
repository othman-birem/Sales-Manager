using Sales_Manager.EntitiesManagement;

namespace Sales_Manager.Services
{
    internal class ServiceBase
    {
        internal SalesManagerContext _context { get; init; }

        internal ServiceBase(DesignTimeDbContextFactory factory)
        {
            _context = factory.CreateDbContext(args: null);
        }
    }
}
