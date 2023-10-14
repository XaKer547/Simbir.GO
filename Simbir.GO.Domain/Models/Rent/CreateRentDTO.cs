namespace Simbir.GO.Domain.Models.Rent
{
    public record CreateRentDTO
    {
        public long TransportId { get; init; }
        public long UserId { get; init; }
        public string TimeStart { get; init; }
        public string? TimeEnd { get; init; }
        public double PriceOfUnit { get; init; }
        public string PriceType { get; init; }
        public double? FinalPrice { get; init; }
    }
}
