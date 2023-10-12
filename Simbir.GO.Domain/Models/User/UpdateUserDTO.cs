using Simbir.GO.Domain.Models.Enums;

namespace Simbir.GO.Domain.Models.User
{
    public record UpdateUserDTO()
    {
        public string Username { get; init; }
        public string Password { get; init; }
        public UserRoles Role { get; init; }
        public double Balance { get; init; }
    }
}
