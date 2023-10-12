using Simbir.GO.Domain.Models.Enums;

namespace Simbir.GO.Domain.Models.User
{
    public record CreateUserDTO()
    {
        public string Username { get; init; }
        public UserRoles Role { get; init; }
        public string Password { get; init; }
        public double Balance { get; init; }
    }
}
