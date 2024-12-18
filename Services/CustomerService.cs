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
            if (!await _context.Customers.AnyAsync())
            {
                string data = File.ReadAllText("C:\\Users\\Othman\\Desktop\\Projects\\3-tier sales manager\\Sales Manager\\Services\\FakeData\\Customers.json");
                Customer[]? objs = JsonConvert.DeserializeObject<Customer[]>(data);

                await _context.Customers.AddRangeAsync(objs);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Customer> Add(Customer obj)
        {
            var customer = await _context.Customers.AddAsync(obj);
            await _context.SaveChangesAsync();

            return customer.Entity;
        }
        public async Task<List<Customer>> GetAsync()
        {
            await Task.Delay(200);
            return await _context.Customers.AsNoTracking().ToListAsync();
        }
        public List<Customer> Get()
        {
            return _context.Customers.ToList();
        }
        public async Task Delete(Customer obj)
        {
            if (obj != null)
            {
                _context.Customers.Remove(obj);
                await _context.SaveChangesAsync();
            }
        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
