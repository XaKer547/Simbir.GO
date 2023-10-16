using Simbir.GO.Domain.Models.Rent;

namespace Simbir.GO.Domain.Services
{
    public interface IRentService
    {
        Task<RentDTO> GetRentAsync(long id);
        Task<long> GetRentedTransportAsync(long rentId);
        Task<IReadOnlyCollection<RentDTO>> GetUserRentHistoryAsync(long userId);
        Task<IReadOnlyCollection<RentDTO>> GetTrasnportRentHistoryAsync(long transportId);
        Task<bool> IsTenantAsync(long userId, long rentId);
        Task CreateRentAsync(CreateRentDTO dto);
        Task StartRentAsync(StartRentDTO dto);
        Task UpdateRentAsync(UpdateRentDTO dto);
        Task EndRentAsync(EndRentDTO dto);
        Task DeleteRentAsync(long rentId);
    }
}
