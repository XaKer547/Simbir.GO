using System.ComponentModel.DataAnnotations.Schema;

namespace Simbir.GO.DataAccess.Data.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public double Balance { get; set; }
        public int RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }
        public ICollection<Transport> Transports { get; set; } = new HashSet<Transport>();
        public ICollection<Rent> Rents { get; set; } = new HashSet<Rent>();
    }
}
