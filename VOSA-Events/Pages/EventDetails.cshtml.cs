using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
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

		//Metoder
		public void OnGet(int id)
		{
			Event = database.Events.Find(id);
		}
		public IActionResult OnPostOrder(int quantity, int id )
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

		public IActionResult OnPostFollow(int id)
		{
			var loggedInUserId = accessControl.LoggedInAccountID;

				// Om användaren inte redan följer evenemanget, lägg till följningen
				var follows = new Follow
				{
					EventID = id,
					AccountID = loggedInUserId,
				};

				database.Follows.Add(follows);
				database.SaveChanges();

			return RedirectToPage("/Index");
		}

		public bool IsEventFollowed(int eventId)
		{
			var loggedInUserId = accessControl.LoggedInAccountID;

			// Kontrollera om användaren redan följer detta evenemang
			var existingFollow = database.Follows.Any(f => f.AccountID == loggedInUserId && f.EventID == eventId);

			return existingFollow;
		}

	}
}
