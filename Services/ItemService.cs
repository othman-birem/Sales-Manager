using Sales_Manager.EntitiesManagement;
using Sales_Manager.Models;

namespace Sales_Manager.Services
{
    internal class ItemService : ServiceBase, IService<Item>
    {
        internal ItemService(DesignTimeDbContextFactory factory) : base(factory)
        {

        }

        public Task Add(Item obj)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Item>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(Item entity)
        {
            throw new NotImplementedException();
        }
    }
}
