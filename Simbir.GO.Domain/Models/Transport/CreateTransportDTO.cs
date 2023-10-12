namespace Simbir.GO.Domain.Models.Transport
{
    public record CreateTransportDTO
    {
        public CreateTransportDTO((string transportType, bool canBeRented, string model, string color, string identifier, string? description, double latitude, double longtitude, double? minutePrice, double? dayPrice) transportDto)
        {
            (TransportType, CanBeRented, Model, Color, Identifier, Description, Latitude, Longitude, MinutePrice, DayPrice) = transportDto;
        }

        public int OwnerId { get; init; }
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
