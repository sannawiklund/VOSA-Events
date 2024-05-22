using Microsoft.AspNetCore.Mvc.RazorPages;
using VOSA_Events.Data;
using VOSA_Events.Models;

namespace VOSA_Events.Pages
{
	public class IndexModel : PageModel
	{
		private readonly AppDbContext database;
		public IndexModel(AppDbContext database)
		{
			this.database = database;
		}

		public List<Event> Events { get; set; }
		public int PageNumber { get; set; }
		public int TotalPages { get; set; }
		public string Category { get; set; }


        public void OnGet(int pageNumber, string searchItem, string category, string date)
        {
            PageNumber = pageNumber;
            var pageSize = 4; 

            Events = database.Events.ToList();

            if (!string.IsNullOrEmpty(searchItem))
            {
                Events = Events.Where(e => e.Name.ToLower().Contains(searchItem)).ToList();
            }

            if (!string.IsNullOrEmpty(category) && category != "Alla kategorier")
            {
                Category = category.ToLower();

                var selectedCategoryID = database.Categories
                                                    .FirstOrDefault(c => c.Name.ToLower() == Category)?.ID;

                if (selectedCategoryID != null)
                {
                    Events = Events.Where(e => e.CategoryID == selectedCategoryID).ToList();
                }
            }

            if (!string.IsNullOrEmpty(date) && date == "Mässor som sker snart")
            {
                Events = Events.OrderBy(e => e.Date).ToList();
            }

            if (!string.IsNullOrEmpty(date) && date == "Mässor som sker längre fram")
            {
                Events = Events.OrderByDescending(e => e.Date).ToList();
            }

            int totalEvents = Events.Count;
            TotalPages = (int)Math.Ceiling((double)totalEvents / pageSize);

            if (PageNumber < 1)
            {
                PageNumber = 1;
            }

            Events = Events.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();
        }
    }
}
