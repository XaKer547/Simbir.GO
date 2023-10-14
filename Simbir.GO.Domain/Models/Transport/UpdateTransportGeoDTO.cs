namespace Simbir.GO.Domain.Models.Transport
{
    public record UpdateTransportGeoDTO
    {
        public long TrasnportId { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
    }
}
