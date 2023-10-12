namespace Simbir.GO.DataAccess.Data.DbSeeder
{
    public partial class SimbirGoDbSeeder
    {
        private readonly SimbirGoDbContext _context;
        public SimbirGoDbSeeder(SimbirGoDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await SeedRoles();

            await SeedRentTypes();

            await SeedTransportTypes();
        }
    }
}
