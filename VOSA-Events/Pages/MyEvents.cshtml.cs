using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VOSA_Events.Data;
using VOSA_Events.Models;


namespace VOSA_Events.Pages
{
    public class MyEventsModel : PageModel
    {
        private readonly AppDbContext database;
        private readonly AccessControl accessControl;

        public MyEventsModel(AppDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }

        public List<Booking> BookedEvents { get; set; }
        public List<Event> FollowedEvents { get; set; }


        public void OnGet()
        {
            var loggedInUserId = accessControl.LoggedInAccountID;

            BookedEvents = database.Bookings
                                      .Where(b => b.AccountID == loggedInUserId)
                                      .Include(b => b.Event)
                                      .ToList();

            FollowedEvents = database.Follows
                                       .Where(f => f.AccountID == loggedInUserId)
                                       .Include(f => f.Event)
                                       .Select(f => f.Event)
                                       .ToList();

        }
        public IActionResult OnPostUnfollowEvent(int eventId)
        {
            var loggedInUserId = accessControl.LoggedInAccountID;

            var followedEvent = database.Follows
                                        .FirstOrDefault(f => f.AccountID == loggedInUserId && f.EventID == eventId);

            if (followedEvent != null)
            {
                database.Follows.Remove(followedEvent);
                database.SaveChanges();
            }

            return RedirectToPage("MyEvents");
        }
    }
}
