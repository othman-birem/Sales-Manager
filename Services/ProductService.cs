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

        public Task Add(Product obj)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }

        #region helpers
        private async void INIT()
        {
            if (!_context.Products.Any())
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
