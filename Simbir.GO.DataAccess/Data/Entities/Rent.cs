namespace Simbir.GO.DataAccess.Data.Entities
{
    public class Rent
    {
        public int Id { get; set; }
        public RentType RentType { get; set; }
        public Transport Transport { get; set; }
        public User User { get; set; }
        public bool IsFinished { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
