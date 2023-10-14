using System.ComponentModel.DataAnnotations.Schema;

namespace Simbir.GO.DataAccess.Data.Entities
{
    public class Rent
    {
        public long Id { get; set; }

        public long TransportId { get; set; }
        [ForeignKey(nameof(TransportId))]
        public Transport Transport { get; set; }

        public long UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public int RentTypeId { get; set; }
        [ForeignKey(nameof(RentTypeId))]
        public RentType RentType { get; set; }

        public DateTime TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }

        public double PriceOfUnit { get; set; }
        public double FinalPrice { get; set; } = 0; //сделай вычисляемым
    }
}
