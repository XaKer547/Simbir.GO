using Simbir.GO.Domain.Models.Rent;

namespace Simbir.GO.Domain.Services
{
    public interface IRentService
    {
        Task<IReadOnlyCollection<RentDTO>> GetUserRentHistory(int userId);
        Task<IReadOnlyCollection<RentDTO>> GetTrasnportRentHistory(int transportId);
        Task<bool> IsTenant(int userId, int rentId);

        Task CreateRent(CreateRentDTO dto);

        Task StartRent(StartRentDTO dto);

        Task EndRent();
    }
}
