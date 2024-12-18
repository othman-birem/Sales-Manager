using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sales_Manager.EntitiesManagement;
using Sales_Manager.Models;
using System.IO;

namespace Sales_Manager.Services
{
    internal class OrderService : ServiceBase, IService<Order>
    {
        internal OrderService(DesignTimeDbContextFactory factory) : base(factory)
        {
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
        public async Task<List<Order>> GetAsync()
        {
            await Task.Delay(100);
            return await _context.Orders
                .AsNoTracking()
                .Include(o => o.Customer)
                .ToListAsync();
        }

        public List<Order> Get()
        {
            return _context.Orders.ToList();
        }

        public async Task Delete(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public void Update(Order entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> Add(Order obj)
        {
            return (await _context.Orders.AddAsync(obj)).Entity;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public IEnumerable<Item> GetOrderItems(int id)
        {
            return _context.Items
                .Include(o => o.Product)
                .Include(o => o.Order)
                .Where(a => a.Id == id).ToList();
        }
    }
}
