using Simbir.GO.DataAccess.Data.Entities;

namespace Simbir.GO.DataAccess.Data.DbSeeder
{
    public partial class SimbirGoDbSeeder
    {
        public async Task SeedRentTypes()
        {
            if (_context.RentTypes.Any())
                return;

            var rentTypes = new List<RentType>()
            {
                new RentType()
                {
                    Name = "Minutes"
                },
                new RentType()
                {
                    Name = "Day"
                }
            };

            _context.RentTypes.AddRange(rentTypes);

            await _context.SaveChangesAsync();
        }
    }
}
