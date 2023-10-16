using Simbir.GO.Domain.Models.Enums;

namespace Simbir.GO.Domain.Models.Transport
{
    public record TransportPaginationDTO
    {
        public int Start { get; init; }
        public int Count { get; init; }
        public TransportTypes TransportType { get; init; }
    }
}
