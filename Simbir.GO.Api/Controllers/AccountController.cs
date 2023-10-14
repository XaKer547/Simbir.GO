using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Api.Helpers;
using Simbir.GO.Domain.Models.Account;
using Simbir.GO.Domain.Models.User;
using Simbir.GO.Domain.Services;

namespace Simbir.GO.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }



        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<IActionResult> LoginAsync(SignInDTO dto)
        {
            var result = await _accountService.SignInAsync(dto);

            return Ok(result);
        }



        [AllowAnonymous]
        [HttpPost("SignUp")]
        public async Task<IActionResult> CreateAccountAsync(SignUpDTO dto)
        {
            var isUsernameTaken = await _accountService.IsUsernameExistsAsync(dto.Username);

            if (isUsernameTaken)
                return StatusCode(StatusCodes.Status409Conflict, "Пользователь с таким username уже существует");

            await _accountService.CreateUserAsync(new CreateUserDTO()
            {
                Username = dto.Username,
                Password = dto.Password,
                Role = Domain.Models.Enums.UserRoles.User.ToString(),
                Balance = 0
            });

            return Ok();
        }



        [HttpPost("SignOut")]
        public async Task<IActionResult> LogoutAsync()
        {
            return Ok();
        }



        [HttpGet("Me")]
        public async Task<IActionResult> GetUserData()
        {
            var userId = User.GetId();

            var user = await _accountService.GetUserInfoAsync(userId);

            return Ok(user);
        }



        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUserDataAsync(UserLoginDetailsDTO dto)
        {
            var isUsernameTaken = await _accountService.IsUsernameExistsAsync(dto.Username);

            if (isUsernameTaken)
                return StatusCode(StatusCodes.Status409Conflict, "Пользователь с таким username уже существует");

            var userId = User.GetId();

            await _accountService.UpdateUserLoginDetailsAsync(new UpdateUserLoginDetailsDTO()
            {
                UserId = userId,
                Username = dto.Username,
                Password = dto.Password,
            });

            return Ok();
        }



        [Authorize(Roles = "Admin")]
        [HttpPut("/api/Admin/[controller]/{id}")]
        public async Task<IActionResult> UpdateUserDataAsync(int id, UserDTO dto)
        {
            var isUsernameTaken = await _accountService.IsUsernameExistsAsync(dto.Username);

            if (isUsernameTaken)
                return StatusCode(StatusCodes.Status409Conflict, "Пользователь с таким username уже существует");

            await _accountService.UpdateUserAsync(new UpdateUserDTO()
            {
                UserId = id,
                Username = dto.Username,
                Password = dto.Password,
                Balance = dto.Balance,
                Role = dto.Role,
            });

            return Ok();
        }



        [Authorize(Roles = "Admin")]
        [HttpGet("/api/Admin/[controller]")]
        public async Task<IActionResult> GetUsers(int start, int count)
        {
            var users = await _accountService.GetUsersAsync(start, count);

            return Ok(users);
        }



        [Authorize(Roles = "Admin")]
        [HttpGet("/api/Admin/[controller]/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _accountService.GetUserAsync(id);

            return Ok(user);
        }



        [Authorize(Roles = "Admin")]
        [HttpPost("/api/Admin/[controller]")]
        public async Task<IActionResult> CreateAccountAsync(CreateUserDTO dto)
        {
            var isUsernameTaken = await _accountService.IsUsernameExistsAsync(dto.Username);

            if (isUsernameTaken)
                return StatusCode(StatusCodes.Status409Conflict, "Пользователь с таким username уже существует");

            await _accountService.CreateUserAsync(dto);

            return Ok();
        }



        [Authorize(Roles = "Admin")]
        [HttpDelete("/api/Admin/[controller]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _accountService.DeleteUserAsync(id);

            return Ok();
        }
    }
}
