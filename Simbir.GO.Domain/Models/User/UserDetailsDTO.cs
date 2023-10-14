namespace Simbir.GO.Domain.Models.User
{
    public class UserDetailsDTO
    {
        public long Id { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public double Balance { get; init; }
        public string Role { get; init; }

        public IReadOnlyCollection<long> Transports { get; init; }
    }
}
