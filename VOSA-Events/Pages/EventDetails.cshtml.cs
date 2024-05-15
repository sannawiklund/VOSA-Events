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
		public List<Review> Reviews { get; set; }
		public Booking Booking { get; set; }
		public bool ShowReviews { get; set; }

        //Metoder
        private Event GetEventByID(int id)
        {
            return Event = database.Events.First(e => e.ID == id);
        }

        public void OnGet(int id) // H�mtar detaljer f�r ett specifikt event baserat p� ID. 
        {
            Event = GetEventByID(id);
        }

        public IActionResult OnPost(int id, int quantity)
        {
            Event = GetEventByID(id);

            if (Event != null)
            {
                // Skapa en ny varukorg som tillh�r den nuvarande inloggade anv�ndare om mins ett event blivit bokat. 
                var newBooking = new Booking
                {
                    AccountID = accessControl.LoggedInAccountID,
                    EventID = id,
                    Quantity = quantity // Anv�nd det valda v�rdet f�r kvantiteten
                };

                database.Bookings.Add(newBooking);
                database.SaveChanges();

                return RedirectToPage("/Cart");
            }
            else
            {
                return NotFound();
            }
        }


        public IActionResult OnPostFollow(int id)
		{
			var loggedInUserId = accessControl.LoggedInAccountID;

				var follows = new Follow
				{
					EventID = id,
					AccountID = loggedInUserId,
				};

				database.Follows.Add(follows);
				database.SaveChanges();

			return RedirectToPage("/Index");
		}

		public void LoadReviews(int id)
		{
			Reviews = database.Reviews.Where(r => r.EventID == id).ToList();
		}
		public ActionResult OnGetShowReviews(int id, bool showReviews)
		{
			Event = database.Events.Find(id);
			
			ShowReviews = showReviews;

			if (ShowReviews)
			{
				LoadReviews(id);
			}

			return Page();
		}

		public bool IsEventFollowed(int eventId)
		{
			var loggedInUserId = accessControl.LoggedInAccountID;

			var existingFollow = database.Follows.Any(f => f.AccountID == loggedInUserId && f.EventID == eventId);

			return existingFollow;
		}

	}
}
