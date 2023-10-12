using Simbir.GO.Domain.Models.Transport;

namespace Simbir.GO.Domain.Services
{
    public interface ITransportService
    {
        Task<TransportDTO> GetTransportByIdAsync(int id);
        Task<IReadOnlyCollection<TransportDTO>> GetTransports();
        Task DeleteTransportByIdAsync(int id);
        Task<bool> IsTransportOwner(int userId, int transportId);
        Task CreateTransportAsync(CreateTransportDTO dto);
        Task UpdateTransportAsync(UpdateTransportDTO dto);
    }
}
