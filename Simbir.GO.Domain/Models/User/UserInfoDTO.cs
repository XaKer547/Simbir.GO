namespace Simbir.GO.Domain.Models.User
{
    public record UserInfoDTO
    {
        public string Username { get; init; }
        public double Balance { get; init; }
        public int TransportOwnedCount { get; init; }
    }
}