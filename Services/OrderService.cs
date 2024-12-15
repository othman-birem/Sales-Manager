using Newtonsoft.Json;
using Sales_Manager.EntitiesManagement;
using Sales_Manager.Models;
using System.IO;

namespace Sales_Manager.Services
{
    public class OrderService
    {
        private SalesManagerContext _context;

        internal OrderService(DesignTimeDbContextFactory factory)
        {
            _context = factory.CreateDbContext(null);
            //INIT();
        }

        private async void INIT()
        {
            if (!_context.Orders.Any())
            {
                string data = File.ReadAllText("C:\\Users\\Othman\\Desktop\\Projects\\3-tier sales manager\\Sales Manager\\Services\\FakeData\\Orders.json");
                Order[]? objs = JsonConvert.DeserializeObject<Order[]>(data);

                await _context.Orders.AddRangeAsync(objs);
                await _context.SaveChangesAsync();
            }
        }
        public List<Order> Get()
        {
            var list = _context.Orders.AsEnumerable();
            return list.OfType<Order>().ToList();
        }
    }
}
