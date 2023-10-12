namespace Simbir.GO.Domain.Models.Transport
{
    public record TransportDTO
    {
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

        public (string, bool, string, string, string, string?, double, double, double?, double?) Destruct()
        {
            return (TransportType, CanBeRented, Model, Color, Identifier, Description, Latitude, Longitude, MinutePrice, DayPrice);
        }
    }
}
