using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VOSA_Events.Data;
using VOSA_Events.Models;

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
        public Account Account { get; set; }


        public string ReveiwText { get; set; }
        public int Rating { get; set; }


		//Metoder

		public void OnGet(int eventId)
		{
			Event = database.Events.Find(eventId);

		}

		public ActionResult OnPostAddReview(int eventId, string reviewText, int rating)
        {
            var review = new Review()
            {
                EventID = eventId,
                Description = reviewText,
                Rating = rating
            };

            database.Reviews.Add(review);
            database.SaveChanges();

			return RedirectToPage("/EventDetails", new { eventId });
		}
    }
}