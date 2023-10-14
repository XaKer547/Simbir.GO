using Simbir.GO.Domain.Models.Enums;

namespace Simbir.GO.Domain.Models.Transport
{
    public record TransportFilterDTO
    {
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public double Radius { get; init; }
        public RentTypes RentType { get; init; }
    }
}
