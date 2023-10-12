namespace Simbir.GO.Domain.Models.User
{
    public record UpdateUserDataDTO(string Username, string Password)
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
