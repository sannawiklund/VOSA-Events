using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VOSA_Events.Pages
{
    public class OrderConfirmationModel : PageModel
    {
		public double TotalPrice { get; set; }

		public void OnGet(double totalPrice)
        {
			TotalPrice = totalPrice;
		}
	}
}
