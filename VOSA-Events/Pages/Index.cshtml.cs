using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VOSA_Events.Data;
using VOSA_Events.Models;

namespace VOSA_Events.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext database;

		public List<Event> Events { get; set; }
		public int PageNumber { get; set; }
		public int TotalPages { get; set; }
		public string Category { get; set; }

		public IndexModel(AppDbContext database)
        {
            this.database = database;
        }

        public void OnGet()
        {

        }
    }
}