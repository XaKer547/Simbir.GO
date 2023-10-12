using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Api.Helpers;
using Simbir.GO.Domain.Models.Account;
using Simbir.GO.Domain.Models.User;
using Simbir.GO.Domain.Services;

namespace Simbir.GO.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize/*(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)*/]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        #region Anonimous
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
            var result = await _accountService.SignUpAsync(new CreateUserDTO()
            {
                Username = dto.Username,
                Password = dto.Password,
                Role = Domain.Models.Enums.UserRoles.User,
                Balance = 0
            });

            if (!result)
                return StatusCode(StatusCodes.Status409Conflict, "Пользователь с таким username уже существует");

            return Ok();
        }
        #endregion


        #region Authorized
        [HttpPost("SignOut")]
        public async Task<IActionResult> LogoutAsync()
        {
            return Ok();
        }


        [HttpGet("Me")]
        public async Task<IActionResult> GetUserData()
        {
            return Ok();
        }


        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUserDataAsync(UpdateUserDataDTO dto)
        {
            var userId = User.GetId();

            var result = await _accountService.UpdateUserDataAsync(userId, dto);

            if (!result)
                return StatusCode(StatusCodes.Status409Conflict, "Пользователь с таким username уже существует");

            return Ok();
        }
        #endregion


        #region Admin
        [Authorize(Roles = "Admin")]
        [HttpPut("/api/Admin/[controller]/{id}")]
        public async Task<IActionResult> UpdateUserDataAsync(UpdateUserDTO dto, int id)
        {
            var result = await _accountService.UpdateUserDataAsync(id, dto);

            if (!result)
                return StatusCode(StatusCodes.Status409Conflict, "Пользователь с таким username уже существует");

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("/api/Admin/[controller]")]
        public async Task<IActionResult> GetUserData(int start, int count)
        {
            //var users = await _accountService
            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("/api/Admin/[controller]/{id}")]
        public async Task<IActionResult> GetUserData(int id)
        {
            //var users = await _accountService
            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("/api/Admin/[controller]")]
        public async Task<IActionResult> CreateAccountAsync(CreateUserDTO dto)
        {
            var result = await _accountService.SignUpAsync(dto);

            if (!result)
                return StatusCode(StatusCodes.Status409Conflict, "Пользователь с таким username уже существует");

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("/api/Admin/[controller]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _accountService.DeleteUserAsync(id);

            return Ok();
        }
        #endregion
    }
}
