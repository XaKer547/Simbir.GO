using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Api.Helpers;
using Simbir.GO.Application.Exceptions;
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
        public RentController(IRentService rentService, ITransportService transportService)
        {
            _rentService = rentService;
            _transportService = transportService;
        }

        [AllowAnonymous]
        [HttpGet("Transport")]
        public async Task<IActionResult> GetAvaibleTransport(double lat, double @long, double radius, RentTypes type)
        {

            return Ok();
        }

        [HttpGet("{rentId}")]
        public async Task<IActionResult> GetMyRentInfo(long rentId)
        {
            var userId = User.GetId();

            //блять
            var isOwner = await _transportService.IsTransportOwner(userId);

            var isTenant = await _rentService.IsTenantAsync(userId, rentId);

            if (!isOwner && isTenant)
                return BadRequest();

            var rent = await _rentService.GetRentAsync(rentId);

            return Ok(rent);
        }


        [HttpGet("MyHistory")]
        public async Task<IActionResult> GetRentHistory()
        {
            var userId = User.GetId();

            var history = await _rentService.GetUserRentHistoryAsync(userId);

            return Ok(history);
        }


        [HttpGet("TransportHistory/{transportId}")]
        public async Task<IActionResult> GetMyTransportRentHistory(long transportId)
        {
            var userId = User.GetId();

            var isOwner = await _transportService.IsTransportOwner(userId, transportId);

            if (!isOwner)
                return BadRequest();

            var history = await _rentService.GetTrasnportRentHistoryAsync(transportId);

            return Ok(history);
        }


        [HttpPost("New/{transportId}")]
        public async Task<IActionResult> StartTransportRent(long transportId , RentTypes rentType)
        {
            var userId = User.GetId();

            var isOwner = await _transportService.IsTransportOwner(userId, transportId);

            if (isOwner)
                return BadRequest();

            await _rentService.StartRentAsync(new Domain.Models.Rent.StartRentDTO()
            {
                UserId = userId,
                RentType = rentType,
                TransportId = transportId,
            });

            return Ok();
        }


        [HttpPost("End/{rentId}")]
        public async Task<IActionResult> EndTransportRent(long rentId, double lat, double @long)
        {
            var userId = User.GetId();

            var isTenant = await _rentService.IsTenantAsync(userId, rentId);

            if (!isTenant)
                return BadRequest();

            await _rentService.EndRentAsync(new Domain.Models.Rent.EndRentDTO()
            {
                RentId = rentId,
                Latitude = lat,
                Longitude = @long,
            });

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("/api/Admin/[controller]/{rentId}")]
        public async Task<IActionResult> GetRentInfo(long rentId)
        {
            var rent = await _rentService.GetRentAsync(rentId);

            return Ok(rent);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("/api/Admin/UserHistory/{userId}")]
        public async Task<IActionResult> GetUserRentHistory(long userId)
        {
            var history = await _rentService.GetUserRentHistoryAsync(userId);

            return Ok(history);
        }



        [Authorize(Roles = "Admin")]
        [HttpGet("/api/Admin/TransportHistory/{transportId}")]
        public async Task<IActionResult> GetTransportRentHistory(long transportId)
        {
            var history = await _rentService.GetTrasnportRentHistoryAsync(transportId);

            return Ok(history);
        }



        [Authorize(Roles = "Admin")]
        [HttpPost("/api/Admin/[controller]")]
        public async Task<IActionResult> StartTransportRent() // DTO
        {

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("/api/Admin/[controller]/End/{rentId}")]
        public async Task<IActionResult> EndTransportRent(long rentId) // DTO
        {
            await _rentService.EndRentAsync(new Domain.Models.Rent.EndRentDTO()
            {

            });

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("/api/Admin/[controller]/{id}")]
        public async Task<IActionResult> ChangeRentInfo(long id)
        {

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("/api/Admin/[controller]/{rentId}")]
        public async Task<IActionResult> DeleteRentInfo(long rentId)
        {
            try
            {
                await _rentService.DeleteRentAsync(rentId);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
