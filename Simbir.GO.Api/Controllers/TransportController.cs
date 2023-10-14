using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Api.Helpers;
using Simbir.GO.Domain.Models.Enums;
using Simbir.GO.Domain.Models.Transport;
using Simbir.GO.Domain.Services;

namespace Simbir.GO.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransportController : ControllerBase
    {
        private readonly ITransportService _transportService;
        public TransportController(ITransportService transportService)
        {
            _transportService = transportService;
        }


        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransportShortInfo(int id)
        {
            var transport = await _transportService.GetTransportByIdAsync(id);

            return Ok(transport);
        }


        [HttpPost]
        public async Task<IActionResult> AddTransport(TransportDTO dto)
        {
            var userrId = User.GetId();

            await _transportService.CreateTransportAsync(new CreateTransportDTO(dto.Destruct())
            {
                OwnerId = userrId
            });

            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMyTransport(int id, TransportDTO dto)
        {
            var userId = User.GetId();

            var result = await _transportService.IsTransportOwner(userId, id);

            if (!result)
                return BadRequest();

            await _transportService.UpdateTransportAsync(new UpdateTransportDTO(dto.Destruct())
            {
                TransportId = id
            });

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMyTransport(long id)
        {
            var userId = User.GetId();

            var result = await _transportService.IsTransportOwner(userId, id);

            if (!result)
                return BadRequest();

            await _transportService.DeleteTransportByIdAsync(id);

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("/api/Admin/Transport")]
        public async Task<IActionResult> GetTransport(int start, int count, TransportTypes transportType)
        {

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("/api/Admin/Transport/{id}")]
        public async Task<IActionResult> GetTransportInfo(long id)
        {
            var transport = await _transportService.GetTransportByIdAsync(id);

            return Ok(transport);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("/api/Admin/Transport")]
        public async Task<IActionResult> CreateTransport(CreateTransportDTO dto)
        {
            await _transportService.CreateTransportAsync(dto);

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("/api/Admin/Transport/{id}")]
        public async Task<IActionResult> UpdateTransport(long id, TransportDTO dto)
        {
            await _transportService.UpdateTransportAsync(new UpdateTransportDTO(dto.Destruct())
            {
                TransportId = id,
            });

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("/api/Admin/Transport/{id}")]
        public async Task<IActionResult> DeleteTransport(long id)
        {
            await _transportService.DeleteTransportByIdAsync(id);

            return Ok();
        }
    }
}
