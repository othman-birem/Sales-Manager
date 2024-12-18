using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sales_Manager.EntitiesManagement;
using Sales_Manager.Models;
using System.IO;

namespace Sales_Manager.Services
{
    internal class ProductService : ServiceBase, IService<Product>
    {
        internal ProductService(DesignTimeDbContextFactory factory) : base(factory)
        {
            INIT();
        }

        public Task<Product> Add(Product obj)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Product obj)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetAsync()
        {
            await Task.Delay(100);
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public List<Product> Get()
        {
            return _context.Products.ToList();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }

        #region helpers
        private async Task INIT()
        {
            if (!_context.Products.AnyAsync().Result)
            {
                string data = File.ReadAllText("C:\\Users\\Othman\\Desktop\\Projects\\3-tier sales manager\\Sales Manager\\Services\\FakeData\\Products.json");
                Product[]? objs = JsonConvert.DeserializeObject<Product[]>(data);

                await _context.Products.AddRangeAsync(objs);
                await _context.SaveChangesAsync();
            }
        }

        #endregion
    }
}
