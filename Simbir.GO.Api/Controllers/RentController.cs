using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Domain.Models.Enums;
using Simbir.GO.Domain.Services;

namespace Simbir.GO.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class RentController : ControllerBase
    {
        private readonly IRentService _rentService;
        private readonly ITransportService _transportService;
        public RentController(IRentService rentService)
        {
            _rentService = rentService;
        }

        [AllowAnonymous]
        [HttpGet("Transport")]
        public async Task<IActionResult> GetAvaibleTransport(double lat, double @long, double radius, string type)
        {

            return Ok();
        }

        [HttpGet("{rentId}")]
        public async Task<IActionResult> GetMyRentInfo(int rentId)
        {
            //Только арендатор и владелец транспорта

            return Ok();
        }


        [HttpGet("MyHistory")]
        public async Task<IActionResult> GetRentHistory()
        {

            return Ok();
        }


        [HttpGet("TransportHistory/{transportId}")]
        public async Task<IActionResult> GetMyTransportRentHistory(int transportId)
        {

            return Ok();
        }


        [HttpPost("New/{transportId}")]
        public async Task<IActionResult> StartTransportRent(int transportId, RentTypes rentType)
        {
            //нельзя брать в аренду собственный транспорт

            return Ok();
        }


        [HttpPost("End/{rentId}")]
        public async Task<IActionResult> EndTransportRent(int rentId, double lat, double @long)
        {
            //Только человек который создавал эту аренду
            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("/api/Admin/[controller]/{rentId}")]
        public async Task<IActionResult> GetRentInfo(int rentId)
        {

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("/api/Admin/UserHistory/{userId}")]
        public async Task<IActionResult> GetUserRentHistory(int userId)
        {

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("/api/Admin/TransportHistory/{transportId}")]
        public async Task<IActionResult> GetTransportRentHistory(int transportId)
        {

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("/api/Admin/[controller]")]
        public async Task<IActionResult> StartTransportRent() // DTO
        {

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("/api/Admin/[controller]/End/{rentId}")]
        public async Task<IActionResult> EndTransportRent(int rentId) // DTO
        {

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("/api/Admin/[controller]/{id}")]
        public async Task<IActionResult> ChangeRentInfo(int id)
        {

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("/api/Admin/[controller]/{rentId}")]
        public async Task<IActionResult> DeleteRentInfo(int rentId)
        {

            return Ok();
        }
    }
}
