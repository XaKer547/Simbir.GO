using Simbir.GO.Domain.Models.Enums;

namespace Simbir.GO.Domain.Models.Rent
{
    public record StartRentDTO
    {
        public int TransportId { get; init; }
        public int UserId { get; init; }
        public RentTypes RentType { get; init; }
        public DateTime StartTime { get; init; }
    }
}
