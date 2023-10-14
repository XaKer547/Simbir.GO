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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService, IWebHostEnvironment webHostEnvironment)
        {
            _paymentService = paymentService;
            _webHostEnvironment = webHostEnvironment;
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

            return Ok(new
            {
                AudioLink = Path.Combine(_webHostEnvironment.WebRootPath, "Assets", "cheat_activated.mp3")
            });
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
