using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VOSA_Events.Data;
using VOSA_Events.Models;

namespace VOSA_Events.Pages
{
	public class EventDetailsModel : PageModel
	{

		private readonly AppDbContext database;
		private readonly AccessControl accessControl;


		public EventDetailsModel(AppDbContext database, AccessControl accessControl)
		{
			this.database = database;
			this.accessControl = accessControl;
		}

		public Event Event { get; set; }

		public void OnGet(int id)
		{
			Event = database.Events.Find(id);
		}
		public IActionResult OnPost(int id)
		{

			var loggedInUserId = accessControl.LoggedInAccountID;

			var existingEvent = database.Bookings.SingleOrDefault(ap => ap.AccountID == loggedInUserId && ap.EventID == id);

			if (existingEvent != null)
			{
				existingEvent.Quantity++;
			}

			else
			{
				var bookings = new Booking
				{
					EventID = id,
					AccountID = loggedInUserId,
					Quantity = 1
				};

				database.Bookings.Add(bookings);
			}

			database.SaveChanges();

			return RedirectToPage("/Index");
		}
	}
}
