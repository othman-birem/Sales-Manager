using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sales_Manager.EntitiesManagement;
using Sales_Manager.ViewModels.Navigation;
using Sales_Manager.ViewModels.Pages;
using Sales_Manager.Views.Pages.Authentication;
using System.IO;
using System.Security.Cryptography;
using System.Windows;

namespace Sales_Manager.Services
{
    public class UserService
    {
        private SalesManagerContext _context;

        internal UserService(DesignTimeDbContextFactory factory)
        {
            _context = factory.CreateDbContext(args: null);
            INIT();
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

        internal async Task<bool> Login(string name, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.Name.Equals(name) && a.Password.Equals(Hash(password)));

            return user != null;
        }



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
    }
}
