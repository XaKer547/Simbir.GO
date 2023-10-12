using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Domain.Services;

namespace Simbir.GO.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public PaymentController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("/api/[controller]/Admin/Hesoyam")]
        public async Task<IActionResult> IncreaseEveryonesBalance()
        {
            await _accountService.IncreaseBalance();
            return Ok("cheat activated");
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("Hesoyam/{accountId}")]
        public async Task<IActionResult> IncreaseBalance(int accountId)
        {
            await _accountService.IncreaseBalance(accountId);

            return Ok("cheat activated");
        }

        [HttpPost("Hesoyam")]
        public async Task<IActionResult> IncreaseBalance()
        {
            // await _accountService.IncreaseBalance(1);

            return Ok("cheat activated");
        }
    }
}
