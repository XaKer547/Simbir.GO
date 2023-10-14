﻿namespace Simbir.GO.Domain.Models.User
{
    public record UpdateUserDTO
    {
        public long UserId { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public string Role { get; init; }
        public double Balance { get; init; }
    }
}
