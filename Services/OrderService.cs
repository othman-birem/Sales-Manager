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
            return await _context.Orders.ToListAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Order entity)
        {
            throw new NotImplementedException();
        }

        public Task Add(Order obj)
        {
            throw new NotImplementedException();
        }
    }
}
