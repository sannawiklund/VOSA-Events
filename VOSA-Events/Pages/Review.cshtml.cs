using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VOSA_Events.Data;
using VOSA_Events.Models;
using System.Security.Claims;

namespace VOSA_Events.Pages
{
    public class ReviewModel : PageModel
    {
        //Databas
        private readonly AppDbContext database;

        public ReviewModel(AppDbContext database)
        {
            this.database = database;
        }

        //Variabler
        public Event Event { get; set; }
        public int EventID { get; set; }
        public string ReveiwText { get; set; }
        public int Rating { get; set; }

		//Metoder

		public void OnGet(int eventId)
		{
			Event = database.Events.Find(eventId);
        }
        public ActionResult OnPost(int eventId, string reviewText, int rating)
        {

            var openIdSubject = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var accountId = database.Accounts
                .Where(a => a.OpenIDSubject == openIdSubject)
                .Select(a => a.ID)
                .FirstOrDefault();
            
            
            var review = new Review()
            {
                AccountID = accountId,
                EventID = eventId,
                Description = reviewText,
                Rating = rating
            };

            database.Reviews.Add(review);
            database.SaveChanges();

            return RedirectToPage("./Index");
            /* Vill helst att man blir redirectad till: "./EventDetails", new { eventId }*/
        }
    }   
}