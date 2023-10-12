namespace Simbir.GO.DataAccess.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public double Balance { get; set; }
        public Role Role { get; set; }
        public IReadOnlyCollection<Transport> Transports { get; set; } = new HashSet<Transport>();
        public IReadOnlyCollection<Rent> Rents { get; set; } = new HashSet<Rent>();
    }
}
