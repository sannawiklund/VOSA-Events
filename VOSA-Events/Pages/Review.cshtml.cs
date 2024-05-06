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
        public List<Event> Events { get; set; }
        public Account Account { get; set; }


        public int EventId { get; set; }
        public string ReveiwText { get; set; }
        public int Rating { get; set; }



        //Metoder
        public void OnGet()
        {
            Events = database.Events.ToList();



        }

        public void OnPost()
        {
            var review = new Review()
            {
                EventID = EventId,
                Description = ReveiwText,
                Rating = Rating
            };

            database.Reviews.Add(review);
            database.SaveChanges();
        }
    }
}