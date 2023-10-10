using Simbir.GO.Domain.Models;

namespace Simbir.GO.Domain.Services
{
    public interface ITransportService
    {
        Task<TransportDTO> GetTransportByIdAsync(int id);
        Task DeleteTransportByIdAsync(int id);
    }
}
