using Microsoft.EntityFrameworkCore;
using Sales_Manager.EntitiesManagement;
using Sales_Manager.Models;

namespace Sales_Manager.Services
{
    internal class ItemService : ServiceBase, IService<Item>
    {
        internal ItemService(DesignTimeDbContextFactory factory) : base(factory)
        {

        }

        public Task<Item> Add(Item obj)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Item>> GetAsync()
        {
            await Task.Delay(100);
            return await _context.Items.AsNoTracking().ToListAsync();
        }
        public List<Item> Get()
        {
            return _context.Items.ToList();
        }

        public void Update(Item entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddRangeAsync(IEnumerable<Item> items)
        {
            await _context.Items.AddRangeAsync(items);
        }
    }
}
