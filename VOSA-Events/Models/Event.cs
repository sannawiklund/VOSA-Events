namespace VOSA_Events.Models
{
    public class Event
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public DateTime Date { get; set; }
        public int TicketQuantity { get; set; }

        public string ImagePath { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public int AdminAccountID { get; set; }
        public virtual Account AdminAccount { get; set; }

        public virtual List<Booking> Bookings { get; set; }
    }
}
