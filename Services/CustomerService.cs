using Newtonsoft.Json;
using Sales_Manager.EntitiesManagement;
using Sales_Manager.Models;
using System.IO;

namespace Sales_Manager.Services
{
    public class CustomerService
    {
        private SalesManagerContext _context;

        internal CustomerService(DesignTimeDbContextFactory factory)
        {
            _context = factory.CreateDbContext(null);
            INIT();
        }

        private async void INIT()
        {
            if (!_context.Customers.Any())
            {
                string data = File.ReadAllText("C:\\Users\\Othman\\Desktop\\Projects\\3-tier sales manager\\Sales Manager\\Services\\FakeData\\Customers.json");
                Customer[]? objs = JsonConvert.DeserializeObject<Customer[]>(data);

                await _context.Customers.AddRangeAsync(objs);
                await _context.SaveChangesAsync();
            }
        }
        public async void Add(Customer obj)
        {
            await _context.Customers.AddAsync(obj);
            await _context.SaveChangesAsync();
        }
        public List<Customer> Get()
        {
            return _context.Customers.AsEnumerable().OfType<Customer>().ToList();
        }
        public async Task Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
