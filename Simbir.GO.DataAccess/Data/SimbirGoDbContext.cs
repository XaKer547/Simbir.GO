using Microsoft.EntityFrameworkCore;
using Simbir.GO.DataAccess.Data.Entities;

namespace Simbir.GO.DataAccess.Data
{
    public class SimbirGoDbContext : DbContext
    {
        public SimbirGoDbContext(DbContextOptions options) : base(options) { }


        public DbSet<Role> Roles { get; set; }

    }
}
