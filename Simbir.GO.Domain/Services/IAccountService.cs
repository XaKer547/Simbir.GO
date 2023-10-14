using Simbir.GO.Domain.Models.Account;
using Simbir.GO.Domain.Models.User;

namespace Simbir.GO.Domain.Services
{
    public interface IAccountService
    {
        Task<AuthorizationResult> SignInAsync(SignInDTO signIn);
        Task CreateUserAsync(CreateUserDTO signUp);
        Task UpdateUserAsync(UpdateUserDTO update);
        Task UpdateUserLoginDetailsAsync(UpdateUserLoginDetailsDTO update);
        Task DeleteUserAsync(long userId);
        Task<bool> IsUsernameExistsAsync(string username);
        Task<UserInfoDTO> GetUserInfoAsync(long userId);
        Task<UserDetailsDTO> GetUserAsync(long userId);
        Task<IReadOnlyCollection<UserDetailsDTO>> GetUsersAsync(int start, int count);
    }
}
