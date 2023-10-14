using Microsoft.EntityFrameworkCore;
using Simbir.GO.DataAccess.Data;
using Simbir.GO.DataAccess.Data.Entities;
using Simbir.GO.Domain.Models.Account;
using Simbir.GO.Domain.Models.User;
using Simbir.GO.Domain.Services;

namespace Simbir.GO.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly SimbirGoDbContext _context;
        private readonly IJwtManager _jwtManager;
        public AccountService(SimbirGoDbContext context, IJwtManager jwtManager)
        {
            _context = context;
            _jwtManager = jwtManager;
        }

        public async Task<AuthorizationResult> SignInAsync(SignInDTO signIn)
        {
            var user = await _context.Users.Include(u => u.Role)
                .SingleOrDefaultAsync(u => u.Username == signIn.Username && u.Password == signIn.Password);

            if (user is null)
                return new AuthorizationResult(false, null);

            var token = await _jwtManager.CreateToken(user.Id, user.Role.Name);

            return new AuthorizationResult(true, token);
        }


        public async Task CreateUserAsync(CreateUserDTO signUp)
        {
            var user = new User()
            {
                Username = signUp.Username,
                Password = signUp.Password,
                Balance = signUp.Balance,
                Role = FindRoleByName(signUp.Role),
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();
        }


        public async Task DeleteUserAsync(long userId)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }


        public async Task<bool> IsUsernameExistsAsync(string username)
        {
            var usernameCheck = await _context.Users.AnyAsync(u => u.Username == username);

            if (usernameCheck)
                return true;

            return false;
        }


        public async Task<UserInfoDTO> GetUserInfoAsync(long userId)
        {
            var user = await _context.Users.Include(u => u.Transports)
                .SingleOrDefaultAsync(u => u.Id == userId);

            return new UserInfoDTO
            {
                Username = user.Username,
                Balance = user.Balance,
                TransportOwnedCount = user.Transports.Count,
            };
        }


        public Task<UserDetailsDTO> GetUserAsync(long userId)
        {
            var user = _context.Users.Select(u => new UserDetailsDTO
            {
                Id = u.Id,
                Role = u.Role.Name,
                Username = u.Username,
                Password = u.Password,
                Balance = u.Balance,
                Transports = u.Transports.Select(t => t.Id).ToArray(),
            }).SingleOrDefaultAsync(u => u.Id == userId);

            return user;
        }


        public async Task<IReadOnlyCollection<UserDetailsDTO>> GetUsersAsync(int start, int count)
        {
            var users = await _context.Users.Select(u => new UserDetailsDTO
            {
                Id = u.Id,
                Role = u.Role.Name,
                Username = u.Username,
                Password = u.Password,
                Balance = u.Balance,
                Transports = u.Transports.Select(t => t.Id).ToArray(),
            }).Skip(start)
            .Take(count)
            .ToArrayAsync();

            return users;
        }


        public async Task UpdateUserAsync(UpdateUserDTO update)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == update.UserId);

            if (user is null)
                return;

            user.Username = update.Username;
            user.Password = update.Password;
            user.Balance = update.Balance;
            user.Role = FindRoleByName(update.Role);

            _context.Users.Update(user);

            await _context.SaveChangesAsync();
        }


        public async Task UpdateUserLoginDetailsAsync(UpdateUserLoginDetailsDTO update)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == update.UserId);

            if (user is null)
                return;

            user.Username = update.Username;
            user.Password = update.Password;

            _context.Users.Update(user);

            await _context.SaveChangesAsync();
        }


        private Role FindRoleByName(string roleName)
        {
            return _context.Roles.Single(r => r.Name == roleName);
        }
    }
}
