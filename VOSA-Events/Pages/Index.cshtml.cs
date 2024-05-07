using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
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

		public void OnGet(int pageNumber, string searchItem, string category, string date)
		{
			PageNumber = pageNumber;
			var pageSize = 10;

			Events = database.Events.ToList();

			if (!string.IsNullOrEmpty(searchItem))
			{
				Events = Events.Where(e => e.Name.ToLower().Contains(searchItem)).ToList();
			}

			if (!string.IsNullOrEmpty(category) && category != "Any category")
			{
				Category = category.ToLower();

				// Hämta CategoryID för den valda kategorin
				var selectedCategoryID = database.Categories
												  .FirstOrDefault(c => c.Name.ToLower() == Category)?.ID;

				if (selectedCategoryID != null)
				{
					// Filtrera evenemangen baserat på CategoryID
					Events = Events.Where(e => e.CategoryID == selectedCategoryID).ToList();
				}

			}

			if (!string.IsNullOrEmpty(date) && date == "Closest in time")
			{
				Events = Events.OrderBy(e => e.Date).ToList();

			}

			if (!string.IsNullOrEmpty(date) && date == "Farthest in time")
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
