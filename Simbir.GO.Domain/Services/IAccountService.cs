using Simbir.GO.Domain.Models;

namespace Simbir.GO.Domain.Services
{
    public interface IAccountService
    {
        Task<AuthorizationResult> SignInAsync(SignInDTO signIn);
        Task<bool> SignUpAsync(CreateUserDTO signUp);
        Task<bool> UpdateUserDataAsync(UpdateUserDTO update);
        Task DeleteUserAsync(int userId);
    }
}
