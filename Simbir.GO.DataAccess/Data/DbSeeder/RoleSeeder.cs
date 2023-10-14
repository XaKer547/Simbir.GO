using Simbir.GO.DataAccess.Data.Entities;

namespace Simbir.GO.DataAccess.Data.DbSeeder
{
    public partial class SimbirGoDbSeeder
    {
        public async Task SeedRolesAsync()
        {
            if (_context.Roles.Any())
                return;

            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };

            _context.Roles.AddRange(roles);

            await _context.SaveChangesAsync();
        }
    }
}
