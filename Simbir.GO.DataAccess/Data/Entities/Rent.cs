namespace Simbir.GO.DataAccess.Data.Entities
{
    public class Rent
    {
        public long Id { get; set; }
        public Transport Transport { get; set; }
        public User User { get; set; }

        public DateTime TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }

        public double PriceOfUnit { get; set; }
        public RentType RentType { get; set; }
        public double FinalPrice { get; set; } = 0;
    }
}
