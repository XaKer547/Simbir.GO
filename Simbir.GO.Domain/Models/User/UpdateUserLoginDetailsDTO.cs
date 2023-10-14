namespace Simbir.GO.Domain.Models.User
{
    public record UpdateUserLoginDetailsDTO
    {
        public long UserId { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
    }
}