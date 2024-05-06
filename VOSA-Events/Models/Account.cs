namespace VOSA_Events.Models
{
	public class Account
	{
		public int ID { get; set; }
		public string Name { get; set; } //First and last name
		public string? PhoneNumber { get; set; }
		public bool? IsAdmin { get; set; }
		public int? AdminCode { get; set; }

		public string? Role { get; set; } //Admin eller user

		public virtual List<Booking> Bookings { get; set; }
		public virtual List<Event> BookmarkedEvents { get; set; } //Bevakade events
		public virtual List<Event> AdministeredEvents { get; set; } //Events som admin skapat

		public string OpenIDIssuer { get; set; }
		public string OpenIDSubject { get; set; }

	}
}