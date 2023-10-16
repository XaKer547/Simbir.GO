using Simbir.GO.Domain.Models.Transport;

namespace Simbir.GO.Domain.Services
{
    public interface ITransportService
    {
        Task CreateTransportAsync(CreateTransportDTO dto);
        Task<TransportDTO> GetTransportByIdAsync(long id);
        Task<IReadOnlyCollection<TransportDTO>> GetTransportsAsync();
        Task<IReadOnlyCollection<TransportDTO>> GetTransportsAsync(TransportFilterDTO dto);
        Task DeleteTransportAsync(long id);
        Task<bool> IsTransportOwnerAsync(long userId, long transportId);
        Task UpdateTransportAsync(UpdateTransportDTO dto);
        Task UpdateTransportGeoAsync(UpdateTransportGeoDTO dto);
        Task<IReadOnlyCollection<TransportDTO>> GetTransportsAsync(TransportPaginationDTO dto);
    }
}
