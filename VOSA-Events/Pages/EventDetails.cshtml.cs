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
        public List<Review> Reviews { get; set; }
        public bool ShowReviews { get; set; }


        [BindProperty]
        public int Quantity { get; set; } = 1; // Lägg till denna rad


        public void OnGet(int id)
        {
            Event = database.Events.Find(id);
        }

        public IActionResult OnPostOrder(int quantity, int id)
        {
            var loggedInUserId = accessControl.LoggedInAccountID;
            var existingEvent = database.Bookings.SingleOrDefault(b => b.AccountID == loggedInUserId && b.EventID == id);

            if (existingEvent != null)
            {
                existingEvent.Quantity += quantity;
            }
            else
            {
                var booking = new Booking
                {
                    EventID = id,
                    AccountID = loggedInUserId,
                    Quantity = quantity
                };

                database.Bookings.Add(booking);
            }

            database.SaveChanges();
            return RedirectToPage("/Index");
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

        public bool IsEventFollowed(int eventId)
        {
            var loggedInUserId = accessControl.LoggedInAccountID;

            var existingFollow = database.Follows.Any(f => f.AccountID == loggedInUserId && f.EventID == eventId);

            return existingFollow;
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

    }
}
