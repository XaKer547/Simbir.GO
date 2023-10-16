using Microsoft.EntityFrameworkCore;
using Simbir.GO.Application.Exceptions;
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

        /// <summary>
        /// Создает транспорт
        /// </summary>
        /// <param name="dto">Данные о транспорте</param>
        /// <exception cref="EntityNotFoundException">Не найден тип транспорта или его владелец</exception>
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

            if (user is null)
                throw new EntityNotFoundException(nameof(User));

            user.Transports.Add(transport);

            _context.Users.Update(user);

            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Удаляет транспорт
        /// </summary>
        /// <param name="id">Id транспорта</param>
        /// <exception cref="EntityNotFoundException">Не найден транспорт</exception>
        public async Task DeleteTransportAsync(long id)
        {
            var transport = _context.Transports.SingleOrDefault(t => t.Id == id);

            if (transport is null)
                throw new EntityNotFoundException(nameof(Transport));

            _context.Transports.Remove(transport);

            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Получить транспорт по его Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Данные о транспорте</returns>
        /// <exception cref="EntityNotFoundException">Не найден транспорт</exception>
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

            if (transport is null)
                throw new EntityNotFoundException(nameof(Transport));

            return transport;
        }


        /// <summary>
        /// Получить весь имеющийся транспорт
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<TransportDTO>> GetTransportsAsync()
        {
            return await _context.Transports.Select(t => new TransportDTO
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
            }).ToArrayAsync();
        }


        /// <summary>
        /// Получить доступный транспорт по близости
        /// </summary>
        /// <param name="dto">Фильтр поиска транспорта</param>
        public async Task<IReadOnlyCollection<TransportDTO>> GetTransportsAsync(TransportFilterDTO dto)
        {
            var maxLat = dto.Latitude + dto.Radius;
            var minLat = dto.Longitude - dto.Radius;

            var maxLong = dto.Longitude + dto.Radius;
            var minLong = dto.Longitude - dto.Radius;

            return await _context.Transports.Where(t => t.CanBeRented
            && t.Latitude >= minLat && t.Latitude <= maxLat
            && t.Longitude >= minLong && t.Longitude <= maxLong)
                  .Select(t => new TransportDTO
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
                  }).ToArrayAsync();
        }


        public async Task<IReadOnlyCollection<TransportDTO>> GetTransportsAsync(TransportPaginationDTO dto)
        {
            return await _context.Transports.Where(t => t.TransportType.Id == (int)dto.TransportType)
                .Skip(dto.Start)
                .Take(dto.Count)
                .Select(t => new TransportDTO
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
                }).ToArrayAsync();
        }


        /// <summary>
        /// Является ли данные пользователь владельцем этого транспорта
        /// </summary>
        /// <param name="userId">Id владельца</param>
        /// <param name="transportId">Id транспорта</param>
        /// <returns>
        /// <para>true, если владелец</para>
        /// <para>false, если нет</para>
        /// </returns>
        public async Task<bool> IsTransportOwnerAsync(long userId, long transportId)
        {
            return await _context.Users.AnyAsync(t => t.Id == userId && t.Transports.Any(t => t.Id == userId));
        }


        /// <summary>
        /// Обновить сведения о транспорте
        /// </summary>
        /// <param name="dto">Данные о транспорте</param>
        /// <exception cref="EntityNotFoundException">Транспорт не найден</exception>
        public async Task UpdateTransportAsync(UpdateTransportDTO dto)
        {
            var transport = _context.Transports.SingleOrDefault(t => t.Id == dto.TransportId);

            if (transport is null)
                throw new EntityNotFoundException(nameof(Transport));

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


        /// <summary>
        /// Обновить геолокацию транспорта
        /// </summary>
        /// <param name="dto">Геолокация транспорта</param>
        /// <exception cref="EntityNotFoundException">Транспорт не найден</exception>
        public async Task UpdateTransportGeoAsync(UpdateTransportGeoDTO dto)
        {
            var transport = _context.Transports.SingleOrDefault(t => t.Id == dto.TrasnportId);

            if (transport is null)
                throw new EntityNotFoundException(nameof(Transport));

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