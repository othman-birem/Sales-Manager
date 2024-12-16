using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sales_Manager.EntitiesManagement;
using Sales_Manager.Models;
using System.IO;

namespace Sales_Manager.Services
{
    internal class CustomerService : ServiceBase, IService<Customer>
    {
        internal CustomerService(DesignTimeDbContextFactory factory) : base(factory)
        {
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
        public async Task Add(Customer obj)
        {
            await _context.Customers.AddAsync(obj);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Customer>> GetAsync()
        {
            await Task.Delay(100);
            return await _context.Customers.ToListAsync();
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

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
