using Simbir.GO.DataAccess.Data.Entities;

namespace Simbir.GO.DataAccess.Data.DbSeeder
{
    public partial class SimbirGoDbSeeder
    {
        public async Task SeedTransportTypes()
        {
            if (_context.TransportTypes.Any())
                return;

            var transportTypes = new List<TransportType>()
            {
                new TransportType()
                {
                    Name = "Car"
                },
                new TransportType()
                {
                    Name = "Bike"
                },
                new TransportType()
                {
                    Name = "Scooter"
                }
            };

            _context.TransportTypes.AddRange(transportTypes);

            await _context.SaveChangesAsync();
        }
    }
}
