using Simbir.GO.Domain.Models.Transport;

namespace Simbir.GO.Domain.Services
{
    public interface ITransportService
    {
        Task<TransportDTO> GetTransportByIdAsync(long id);
        Task<IReadOnlyCollection<TransportDTO>> GetTransports();
        Task DeleteTransportByIdAsync(long id);
        Task<bool> IsTransportOwner(long userId, long transportId);
        Task CreateTransportAsync(CreateTransportDTO dto);
        Task UpdateTransportAsync(UpdateTransportDTO dto);
    }
}
