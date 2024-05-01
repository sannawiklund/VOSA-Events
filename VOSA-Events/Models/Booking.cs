namespace VOSA_Events.Models
{
    public class Booking
    {
        public int ID { get; set; }

        public int AccountID { get; set; }
        public virtual Account Account { get; set; }

        public int EventID { get; set; }
        public virtual Event Event { get; set; }

        public int Quantity { get; set; }
    }
}
