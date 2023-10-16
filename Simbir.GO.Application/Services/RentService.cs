using Microsoft.EntityFrameworkCore;
using Simbir.GO.Application.Exceptions;
using Simbir.GO.DataAccess.Data;
using Simbir.GO.DataAccess.Data.Entities;
using Simbir.GO.Domain.Models.Enums;
using Simbir.GO.Domain.Models.Rent;
using Simbir.GO.Domain.Services;

namespace Simbir.GO.Application.Services
{
    public class RentService : IRentService
    {
        private readonly SimbirGoDbContext _context;
        private readonly ITransportService _transportService;
        public RentService(SimbirGoDbContext context, ITransportService transportService)
        {
            _context = context;
            _transportService = transportService;
        }

        /// <summary>
        /// Создать аренду
        /// </summary>
        /// <param name="dto">Сведения о аренде</param>
        public async Task CreateRentAsync(CreateRentDTO dto)
        {
            var rent = new Rent()
            {
                UserId = dto.UserId,
                TransportId = dto.TransportId,
                TimeStart = DateTime.Parse(dto.TimeStart),
                TimeEnd = DateTime.Parse(dto.TimeEnd),
                RentTypeId = (int)dto.RentType,
                PriceOfUnit = dto.PriceOfUnit,
            };

            _context.Rents.Add(rent);

            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Получить историю аренды транспорта
        /// </summary>
        /// <param name="transportId">Id транспорта</param>
        public async Task<IReadOnlyCollection<RentDTO>> GetTrasnportRentHistoryAsync(long transportId)
        {
            return await _context.Rents.Where(r => r.TransportId == transportId)
                .Select(r => new RentDTO
                {
                    Id = r.Id,
                    UserId = r.User.Id,
                    TransportId = r.Transport.Id,
                    TimeStart = r.TimeStart.ToString(),
                    PriceType = r.RentType.Name,
                    FinalPrice = r.FinalPrice,
                    PriceOfUnit = r.PriceOfUnit,
                    TimeEnd = r.TimeEnd.ToString(),
                }).ToArrayAsync();
        }


        /// <summary>
        /// Получить историю аренды пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        public async Task<IReadOnlyCollection<RentDTO>> GetUserRentHistoryAsync(long userId)
        {
            return await _context.Rents.Where(r => r.UserId == userId)
                .Select(r => new RentDTO
                {
                    Id = r.Id,
                    UserId = r.User.Id,
                    TransportId = r.Transport.Id,
                    TimeStart = r.TimeStart.ToString(),
                    PriceType = r.RentType.Name,
                    FinalPrice = r.FinalPrice,
                    PriceOfUnit = r.PriceOfUnit,
                    TimeEnd = r.TimeEnd.ToString(),
                }).ToArrayAsync();
        }


        /// <summary>
        /// Является ли арендатором
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="rentId">Id аренды</param>
        /// <returns></returns>
        public async Task<bool> IsTenantAsync(long userId, long rentId)
        {
            return await _context.Rents.AnyAsync(r => r.Id == rentId && r.User.Id == userId);
        }


        /// <summary>
        /// Начать аренду
        /// </summary>
        /// <param name="dto"></param>
        /// <exception cref="EntityNotFoundException">Транспорт или тип аренды не найден</exception>
        public async Task StartRentAsync(StartRentDTO dto)
        {
            var transport = _context.Transports.SingleOrDefault(t => t.Id == dto.TransportId);

            if (transport is null)
                throw new EntityNotFoundException(nameof(Transport));

            var price = dto.RentType switch
            {
                RentTypes.Day => transport.DayPrice,
                RentTypes.Minutes => transport.MinutePrice,
                _ => throw new EntityNotFoundException(nameof(RentTypes))
            };

            var rent = new Rent()
            {
                Transport = transport,
                UserId = dto.UserId,
                RentTypeId = (int)dto.RentType,
                TimeStart = DateTime.Now,
                PriceOfUnit = (double)price,
            };

            _context.Rents.Add(rent);

            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Завершить аредну
        /// </summary>
        /// <param name="dto"></param>
        /// <exception cref="EntityNotFoundException">Аренда не найдена</exception>
        public async Task EndRentAsync(EndRentDTO dto)
        {
            var rent = _context.Rents.SingleOrDefault(r => r.Id == dto.RentId);

            if (rent is null)
                throw new EntityNotFoundException(nameof(Rent));

            rent.TimeEnd = DateTime.Now;

            _context.Rents.Update(rent);

            await _context.SaveChangesAsync();

            await _transportService.UpdateTransportGeoAsync(new Domain.Models.Transport.UpdateTransportGeoDTO()
            {
                TrasnportId = rent.TransportId,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
            });
        }


        /// <summary>
        /// Получить сведения о аренде
        /// </summary>
        /// <param name="id">Id аренды</param>
        /// <exception cref="EntityNotFoundException">Аренда не найдена</exception>
        public async Task<RentDTO> GetRentAsync(long id)
        {
            var rent = await _context.Rents.Include(r => r.RentType)
                .SingleOrDefaultAsync(r => r.Id == id);

            if (rent is null)
                throw new EntityNotFoundException(nameof(Rent));

            return new RentDTO()
            {
                UserId = rent.UserId,
                TransportId = rent.TransportId,
                TimeStart = rent.TimeStart.ToString(),
                PriceType = rent.RentType.Name,
                FinalPrice = rent.FinalPrice,
                PriceOfUnit = rent.PriceOfUnit,
                TimeEnd = rent.TimeEnd.ToString(),
            };
        }


        /// <summary>
        /// Удалить аренду
        /// </summary>
        /// <param name="rentId">Id аренды</param>
        /// <exception cref="EntityNotFoundException">Аренда не найденв</exception>
        public async Task DeleteRentAsync(long rentId)
        {
            var rent = _context.Rents.SingleOrDefault(r => r.Id == rentId);

            if (rent is null)
                throw new EntityNotFoundException(nameof(Rent));

            _context.Rents.Remove(rent);

            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Получить арендованный транспорт
        /// </summary>
        /// <param name="rentId">Id аренды</param>
        /// <returns>id транспорта</returns>
        /// <exception cref="EntityNotFoundException">Аренда не найдена</exception>
        public async Task<long> GetRentedTransportAsync(long rentId)
        {
            var transport = await _transportService.GetTransportByIdAsync(rentId);

            if (transport is null)
                throw new EntityNotFoundException(nameof(Transport));

            return transport.Id;
        }


        /// <summary>
        /// Обновить сведения о аренде
        /// </summary>
        /// <param name="dto">Данные о аренде</param>
        /// <exception cref="EntityNotFoundException">Аренда не найдена</exception>
        public async Task UpdateRentAsync(UpdateRentDTO dto)
        {
            var rent = _context.Rents.SingleOrDefault(r => r.Id == dto.RentId);

            if (rent is null)
                throw new EntityNotFoundException(nameof(Rent));

            rent.UserId = dto.UserId;
            rent.TransportId = dto.TransportId;
            rent.TimeStart = DateTime.Parse(dto.TimeStart);
            rent.TimeEnd = DateTime.Parse(dto.TimeEnd);
            rent.PriceOfUnit = dto.PriceOfUnit;

            _context.Rents.Update(rent);

            await _context.SaveChangesAsync();
        }
    }
}
