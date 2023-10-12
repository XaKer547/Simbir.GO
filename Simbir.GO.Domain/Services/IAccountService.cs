using Simbir.GO.Domain.Models.Account;
using Simbir.GO.Domain.Models.User;

namespace Simbir.GO.Domain.Services
{
    public interface IAccountService
    {
        Task<AuthorizationResult> SignInAsync(SignInDTO signIn);
        Task<bool> SignUpAsync(CreateUserDTO signUp);
        Task<bool> UpdateUserDataAsync(int userId, UpdateUserDTO update);
        Task<bool> UpdateUserDataAsync(int userId, UpdateUserDataDTO update);
        Task DeleteUserAsync(int userId);
        Task IncreaseBalance();
        Task IncreaseBalance(int userId);
    }
}
