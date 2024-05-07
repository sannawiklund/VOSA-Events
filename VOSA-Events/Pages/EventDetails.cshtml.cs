using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VOSA_Events.Data;
using VOSA_Events.Models;

namespace VOSA_Events.Pages
{
	public class EventDetailsModel : PageModel
	{
		//Databas
		private readonly AppDbContext database;
		private readonly AccessControl accessControl;


		public EventDetailsModel(AppDbContext database, AccessControl accessControl)
		{
			this.database = database;
			this.accessControl = accessControl;
		}

		//Variabler
		public Event Event { get; set; }
		public List<Event> Events { get; set; }

		//Metoder
		public void OnGet(int id)
		{
			Event = database.Events.Find(id);
		}
		public IActionResult OnPost(int quantity, int id )
		{

			var loggedInUserId = accessControl.LoggedInAccountID;

			var existingEvent = database.Bookings.SingleOrDefault(b => b.AccountID == loggedInUserId && b.EventID == id);

			if (existingEvent != null)
			{
				existingEvent.Quantity += quantity;
			}

			else
			{
				var bookings = new Booking
				{
					EventID = id,
					AccountID = loggedInUserId,
					Quantity = quantity
				};

				database.Bookings.Add(bookings);
			}

			database.SaveChanges();

			return RedirectToPage("/Index");
		}
	}
}
