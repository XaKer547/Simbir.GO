using Microsoft.EntityFrameworkCore;
using Simbir.GO.DataAccess.Data;
using Simbir.GO.DataAccess.Data.Entities;
using Simbir.GO.Domain.Models.Rent;
using Simbir.GO.Domain.Services;

namespace Simbir.GO.Application.Services
{
    public class RentService : IRentService
    {
        private readonly SimbirGoDbContext _context;
        public RentService(SimbirGoDbContext context)
        {
            _context = context;
        }

        public async Task CreateRent(CreateRentDTO dto)
        {
            var rent = new Rent()
            {
                User = dto.UserId,
                Transport = dto.TransportId,
                TimeStart = DateTime.Now,
                RentType = dto.RentType,
                PriceOfUnit = dto.PriceOfUnit,
            };

            _context.Rents.Add(rent);

            await _context.SaveChangesAsync();
        }

        public Task EndRent()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<RentDTO>> GetTrasnportRentHistory(int transportId)
        {
            //давай конвертить в зависимости от типа отплаты
            var history = _context.Rents.Where(r => r.Transport.Id == transportId)
                .Select(r => new RentDTO
                {
                    UserId = r.User.Id,
                    TransportId = r.Transport.Id,
                    TimeStart = r.TimeStart.ToS,
                    PriceType = r.RentType.Name,
                    FinalPrice = r.FinalPrice,
                    PriceOfUnit = r.PriceOfUnit,
                    TimeEnd = r.TimeEnd,
                });
        }

        public Task<IReadOnlyCollection<RentDTO>> GetUserRentHistory(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsTenant(int userId, int rentId)
        {
            var rent = await _context.Rents.SingleOrDefaultAsync(r => r.Id == rentId && r.User.Id == userId);

            return rent != null;
        }

        public Task StartRent(StartRentDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
