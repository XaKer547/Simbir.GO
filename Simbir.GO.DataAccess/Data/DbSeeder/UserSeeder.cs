using Simbir.GO.DataAccess.Data.Entities;

namespace Simbir.GO.DataAccess.Data.DbSeeder
{
    public partial class SimbirGoDbSeeder
    {
        public async Task SeedUsersAsync()
        {
            if (_context.Users.Any())
                return;

            var users = new List<User>()
            {
                new User()
                {
                    Username = "Alex",
                    Password = "superSecurePassword",
                    RoleId = 1,
                    Balance = 547,
                },
                new User()
                {
                    Username = "Pedro",
                    Password = "SusyBaka",
                    RoleId = 2,
                    Balance = 0
                }
            };

            _context.Users.AddRange(users);

            await _context.SaveChangesAsync();
        }
    }
}
