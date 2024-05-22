using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VOSA_Events.Data;
using VOSA_Events.Models;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace VOSA_Events.Pages
{
    public class ReviewModel : PageModel
    {
        private readonly AppDbContext database;

        public ReviewModel(AppDbContext database)
        {
            this.database = database;
        }

        public Event Event { get; set; }
        public int EventID { get; set; }
        public string ReveiwText { get; set; }
        public int Rating { get; set; }
        public bool ShowUpdateMessage { get; set; }


        private int GetUserId()
        {
            var openIdSubject = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return database.Accounts
                .Where(a => a.OpenIDSubject == openIdSubject)
                .Select(a => a.ID)
                .FirstOrDefault();
        }

        private Review GetReview(int eventId, int accountId)
        {
            return database.Reviews
                .FirstOrDefault(r => r.EventID == eventId && r.AccountID == accountId);
        }
        private bool CheckForExistingReview(int eventId, int accountId)
        {
            return GetReview(eventId, accountId) != null;
        }
        
        //Plockar bort html-formatteringen
        private string CleanUpHtml(string input)
        {
            if (input != null)
            {
            return Regex.Replace(input, "<.*?>", String.Empty);
            }
            else
            {
                return input;
            }
        }

        public void OnGet(int eventId)
		{
			Event = database.Events.Find(eventId);

            var accountId = GetUserId();

            //Undersöker ifall användaren redan har en review på eventet och visar isf ett meddelande
            ShowUpdateMessage = CheckForExistingReview(eventId, accountId);

        }
        public ActionResult OnPost(int eventId, string reviewText, int rating)
        {
            var accountId = GetUserId();

            reviewText = CleanUpHtml(reviewText);

            var existingReview = GetReview(eventId, accountId);

            if (existingReview != null)
            {
                existingReview.Description = reviewText;
                existingReview.Rating = rating;
            }
            else
            {
                var review = new Review()
                {
                    AccountID = accountId,
                    EventID = eventId,
                    Description = reviewText,
                    Rating = rating
                };

                database.Reviews.Add(review);
            }

            database.SaveChanges();

            return RedirectToPage("./Index");
            /* Vill helst att man blir redirectad till: "./EventDetails", new { eventId }*/
        }
    }   
}