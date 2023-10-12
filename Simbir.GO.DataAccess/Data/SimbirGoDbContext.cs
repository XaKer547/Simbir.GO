using Microsoft.EntityFrameworkCore;
using Simbir.GO.DataAccess.Data.Entities;

namespace Simbir.GO.DataAccess.Data
{
    public class SimbirGoDbContext : DbContext
    {
        public SimbirGoDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Transport> Transports { get; set; }
        public DbSet<TransportType> TransportTypes { get; set; }
        public DbSet<RentType> RentTypes { get; set; }

        public DbSet<Rent> Rents { get; set; }
    }
}
