using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Domain.Models;
using Simbir.GO.Domain.Services;
using System.Security.Claims;

namespace Simbir.GO.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #region User
        [HttpGet("Me")]
        public async Task<IActionResult> GetUserData()
        {
            return Ok();
        }

        [HttpPut("Update")]//проверь что будет если ты вырежешь из body какую-то часть и отправишь такой токен
        public async Task<IActionResult> UpdateUserDataAsync(UpdateUserDataDTO dto)
        {
            var userIdClaim = User.Claims.SingleOrDefault(u => u.Value == ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return BadRequest();

            var result = await _accountService.UpdateUserDataAsync(new UpdateUserDTO()
            {
                UsertId = int.Parse(userIdClaim.Value),
                Username = dto.Username,
                Password = dto.Password,
            });

            if (!result)
                return StatusCode(StatusCodes.Status409Conflict, "Пользователь с таким username уже существует");

            return Ok();
        }

        [HttpPost("SignOut")]
        public async Task<IActionResult> LogoutAsync()
        {
            return Ok();
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

        [HttpGet("/api/Admin/[controller]")]
        public async Task<IActionResult> GetUserData(int start, int count)
        {
            //var users = await _accountService
            return Ok();
        }

        [HttpGet("/api/Admin/[controller]/{id}")]
        public async Task<IActionResult> GetUserData(int id)
        {
            //var users = await _accountService
            return Ok();
        }

        [HttpPost("/api/Admin/[controller]")]
        public async Task<IActionResult> CreateAccountAsync(CreateUserDTO dto)
        {
            var result = await _accountService.SignUpAsync(dto);

            if (!result)
                return StatusCode(StatusCodes.Status409Conflict, "Пользователь с таким username уже существует");

            return Ok();
        }

        [HttpPut("/api/Admin/[controller]/{id}")]//тут должна быть совершенно другая DTO
        public async Task<IActionResult> UpdateUserDataAsync(UpdateUserDataDTO dto, int id)
        {
            var result = await _accountService.UpdateUserDataAsync(new UpdateUserDTO()
            {
                UsertId = id,
                Username = dto.Username,
                Password = dto.Password,
            });

            if (!result)
                return StatusCode(StatusCodes.Status409Conflict, "Пользователь с таким username уже существует");

            return Ok();
        }

        [HttpDelete("/api/Admin/[controller]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _accountService.DeleteUserAsync(id);

            return Ok();
        }
    }
}
