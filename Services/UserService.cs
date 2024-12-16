using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sales_Manager.EntitiesManagement;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Sales_Manager.Services
{
    internal class UserService : ServiceBase, IService<User>
    {
        internal UserService(DesignTimeDbContextFactory factory) : base(factory)
        {
            INIT();
        }
        public async Task<List<User>> GetAsync()
        {
            await Task.Delay(100);
            return await _context.Users.AsNoTracking().ToListAsync();
        }
        public List<User> Get()
        {
            return _context.Users.ToList();
        }
        public async Task Delete(int id)
        {
            var obj = await _context.Users.FindAsync(id);

            if (obj != null)
            {
                _context.Users.Remove(obj);
                await _context.SaveChangesAsync();
            }
        }
        public void Update(User user)
        {
            throw new NotImplementedException();
        }
        public Task<User> Add(User obj)
        {
            throw new NotImplementedException();
        }

        #region Helpers
        public static string Hash(string raw_password)
        {
            using (var sha = SHA512.Create())
            {
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(raw_password);
                byte[] hashBytes = sha.ComputeHash(textBytes);
                string hash = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", String.Empty);

                return hash;
            }
        }
        internal async Task<bool> Login(string name, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.Name.Equals(name) && a.Password.Equals(Hash(password)));

            return user != null;
        }
        private async void INIT()
        {
            if (!_context.Users.Any())
            {
                string data = File.ReadAllText("C:\\Users\\Othman\\Desktop\\Projects\\3-tier sales manager\\Sales Manager\\Services\\FakeData\\Users.json");
                User[]? objs = JsonConvert.DeserializeObject<User[]>(data);

                for (int i = 0; i < objs.Length; i++) { objs[i].Password = Hash(objs[i].Password); }

                await _context.Users.AddRangeAsync(objs);
                await _context.SaveChangesAsync();
            }
        }
        #endregion

    }
}
