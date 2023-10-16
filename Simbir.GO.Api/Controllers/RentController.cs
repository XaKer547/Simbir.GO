using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Api.Helpers;
using Simbir.GO.Application.Exceptions;
using Simbir.GO.Domain.Models.Enums;
using Simbir.GO.Domain.Models.Rent;
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
            var transports = await _transportService.GetTransportsAsync(new Domain.Models.Transport.TransportFilterDTO()
            {
                Latitude = lat,
                Longitude = @long,
                Radius = radius,
                RentType = type
            });

            return Ok(transports);
        }


        [HttpGet("{rentId}")]
        public async Task<IActionResult> GetRentInfo(long rentId)
        {
            var userId = User.GetId();

            var isTenant = await _rentService.IsTenantAsync(userId, rentId);

            try
            {
                var transportId = await _rentService.GetRentedTransportAsync(rentId);

                var isOwner = await _transportService.IsTransportOwnerAsync(userId, transportId);

                if (!isOwner && !isTenant)
                    return BadRequest();

                var rent = await _rentService.GetRentAsync(rentId);

                return Ok(rent);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }


        [HttpGet("MyHistory")]
        public async Task<IActionResult> GetRentHistory()
        {
            var userId = User.GetId();

            try
            {
                var history = await _rentService.GetUserRentHistoryAsync(userId);

                return Ok(history);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }


        [HttpGet("TransportHistory/{transportId}")]
        public async Task<IActionResult> GetMyTransportRentHistory(long transportId)
        {
            var userId = User.GetId();

            var isOwner = await _transportService.IsTransportOwnerAsync(userId, transportId);

            if (!isOwner)
                return BadRequest();

            try
            {
                var history = await _rentService.GetTrasnportRentHistoryAsync(transportId);

                return Ok(history);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }


        [HttpPost("New/{transportId}")]
        public async Task<IActionResult> StartTransportRent(long transportId, RentTypes rentType)
        {
            var userId = User.GetId();

            var isOwner = await _transportService.IsTransportOwnerAsync(userId, transportId);

            if (isOwner)
                return BadRequest();

            try
            {
                await _rentService.StartRentAsync(new StartRentDTO()
                {
                    UserId = userId,
                    RentType = rentType,
                    TransportId = transportId,
                });
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }

            return Ok();
        }


        [HttpPost("End/{rentId}")]
        public async Task<IActionResult> EndTransportRent(long rentId, double lat, double @long)
        {
            var userId = User.GetId();

            var isTenant = await _rentService.IsTenantAsync(userId, rentId);

            if (!isTenant)
                return BadRequest();

            try
            {
                await _rentService.EndRentAsync(new EndRentDTO()
                {
                    RentId = rentId,
                    Latitude = lat,
                    Longitude = @long,
                });
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("/api/Admin/[controller]/{rentId}")]
        public async Task<IActionResult> GetRent(long rentId)
        {
            try
            {
                var rent = await _rentService.GetRentAsync(rentId);

                return Ok(rent);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("/api/Admin/UserHistory/{userId}")]
        public async Task<IActionResult> GetUserRentHistory(long userId)
        {
            try
            {
                var history = await _rentService.GetUserRentHistoryAsync(userId);

                return Ok(history);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
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
        public async Task<IActionResult> CreateTransportRent(CreateRentDTO dto)
        {
            try
            {
                await _rentService.CreateRentAsync(dto);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("/api/Admin/[controller]/End/{rentId}")]
        public async Task<IActionResult> EndRent(long rentId, double lat, double @long)
        {
            try
            {
                await _rentService.EndRentAsync(new EndRentDTO()
                {
                    RentId = rentId,
                    Latitude = lat,
                    Longitude = @long,
                });
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("/api/Admin/[controller]/{id}")]
        public async Task<IActionResult> ChangeRentInfo(long id, RentDTO dto)
        {
            try
            {
                await _rentService.UpdateRentAsync(new UpdateRentDTO()
                {
                    RentId = id,
                    UserId = dto.UserId,
                    TransportId = dto.TransportId,
                    TimeEnd = dto.TimeEnd,
                    TimeStart = dto.TimeStart,
                    FinalPrice = dto.FinalPrice,
                    PriceOfUnit = dto.PriceOfUnit,
                    PriceType = dto.PriceType,
                });
            }
            catch (EntityNotFoundException)
            {
                return BadRequest();
            }

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
