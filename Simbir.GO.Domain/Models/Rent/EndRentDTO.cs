namespace Simbir.GO.Domain.Models.Rent
{
    public record EndRentDTO
    {
        public long RentId { get; set; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
    }
}
