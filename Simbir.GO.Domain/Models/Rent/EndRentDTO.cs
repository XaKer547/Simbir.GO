using Simbir.GO.Domain.Models.Enums;

namespace Simbir.GO.Domain.Models.Rent
{
    public record EndRentDTO
    {
        public long TransportId { get; init; }
        public long UserId { get; init; }
        public RentTypes RentType { get; init; }
        public DateTime EndTime { get; init; }

        public double Latitude { get; init; }
        public double Longitude { get; init; }
    }
}
