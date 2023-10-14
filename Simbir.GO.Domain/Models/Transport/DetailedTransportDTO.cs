namespace Simbir.GO.Domain.Models.Transport
{
    public record DetailedTransportDTO
    {
        public long Id { get; init; }
        public long OwnerId { get; init; }
        public string TransportType { get; init; }
        public bool CanBeRented { get; init; }
        public string Model { get; init; }
        public string Color { get; init; }
        public string Identifier { get; init; }
        public string? Description { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public double? MinutePrice { get; init; }
        public double? DayPrice { get; init; }
    }
}
