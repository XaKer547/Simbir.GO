using Microsoft.EntityFrameworkCore;
using Simbir.GO.DataAccess.Data;
using Simbir.GO.DataAccess.Data.Entities;
using Simbir.GO.Domain.Models.Transport;
using Simbir.GO.Domain.Services;

namespace Simbir.GO.Application.Services
{
    public class TransportService : ITransportService
    {
        private readonly SimbirGoDbContext _context;
        public TransportService(SimbirGoDbContext context)
        {
            _context = context;
        }

        public async Task CreateTransportAsync(CreateTransportDTO dto)
        {
            var transport = new Transport()
            {
                CanBeRented = dto.CanBeRented,
                Color = dto.Color,
                DayPrice = dto.DayPrice,
                Description = dto.Description,
                Identifier = dto.Identifier,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                MinutePrice = dto.MinutePrice,
                Model = dto.Model,
                TransportType = FindTransportTypeByName(dto.TransportType),
            };

            var user = _context.Users.SingleOrDefault(u => u.Id == dto.OwnerId);

            user.Transports.Add(transport);

            _context.Users.Update(user);

            await _context.SaveChangesAsync();
        }


        public async Task DeleteTransportByIdAsync(long id)
        {
            var transport = _context.Transports.SingleOrDefault(t => t.Id == id);

            _context.Transports.Remove(transport);

            await _context.SaveChangesAsync();
        }


        public async Task<TransportDTO> GetTransportByIdAsync(long id)
        {
            var transport = await _context.Transports.Select(t => new TransportDTO()
            {
                Id = t.Id,
                CanBeRented = t.CanBeRented,
                Color = t.Color,
                DayPrice = t.DayPrice,
                Description = t.Description,
                Identifier = t.Identifier,
                Latitude = t.Latitude,
                Longitude = t.Longitude,
                MinutePrice = t.MinutePrice,
                Model = t.Model,
                TransportType = t.TransportType.Name,
            }).SingleOrDefaultAsync(t => t.Id == id);

            return transport;
        }

        public Task<IReadOnlyCollection<TransportDTO>> GetTransports()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsTransportOwner(long userId, long transportId)
        {
            var transport = await _context.Transports.SingleOrDefaultAsync(t => t.Id == transportId && t.Owner.Id == userId);

            return transport != null;
        }

        public async Task UpdateTransportAsync(UpdateTransportDTO dto)
        {
            var transport = _context.Transports.SingleOrDefault(t => t.Id == dto.TransportId);

            transport.TransportType = FindTransportTypeByName(dto.TransportType);
            transport.DayPrice = dto.DayPrice;
            transport.MinutePrice = dto.MinutePrice;
            transport.CanBeRented = dto.CanBeRented;
            transport.Identifier = dto.Identifier;
            transport.Model = dto.Model;
            transport.Color = dto.Color;
            transport.Description = dto.Description;
            transport.Latitude = dto.Latitude;
            transport.Longitude = dto.Longitude;

            _context.Transports.Update(transport);

            await _context.SaveChangesAsync();
        }

        private TransportType FindTransportTypeByName(string name)
        {
            return _context.Transports.Select(t => t.TransportType)
                .Single(t => t.Name == name);
        }
    }
}
