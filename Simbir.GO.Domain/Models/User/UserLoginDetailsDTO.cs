namespace Simbir.GO.Domain.Models.User
{
    public record UserLoginDetailsDTO
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
