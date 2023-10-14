using Simbir.GO.Domain.Models.Enums;

namespace Simbir.GO.Domain.Models.Rent
{
    public record StartRentDTO
    {
        public long UserId { get; init; }
        public long TransportId { get; init; }
        public RentTypes RentType { get; init; }
    }
}
