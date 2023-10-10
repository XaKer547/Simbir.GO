namespace Simbir.GO.Domain.Models
{
    public record UpdateUserDTO()
    {
        public int UsertId { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
