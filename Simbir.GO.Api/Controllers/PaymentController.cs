using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Api.Helpers;
using Simbir.GO.Domain.Services;

namespace Simbir.GO.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("/api/[controller]/Admin/Hesoyam")]
        public async Task<IActionResult> IncreaseEveryonesBalance()
        {
            await _paymentService.IncreaseBalance();
            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("/api/[controller]/Admin/Hesoyam/{accountId}")]
        public async Task<IActionResult> IncreaseBalance(int accountId)
        {
            await _paymentService.IncreaseBalance(accountId);

            return Ok();
        }

        [HttpPost("Hesoyam")]
        public async Task<IActionResult> IncreaseBalance()
        {
            var userId = User.GetId();

            await _paymentService.IncreaseBalance(userId);

            return Ok();
        }
    }
}
