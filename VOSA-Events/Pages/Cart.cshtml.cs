using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VOSA_Events.Models;
using VOSA_Events.Data;

namespace VOSA_Events.Pages
{
    public class CartModel : PageModel
    {
        public List<Booking> Bookings { get; set; }
        public Event Events { get; set; }
        public int CountItems { get; set; }
        public double TotalPrice { get; set; }
        public string emptyCart { get; set; }


        private readonly AppDbContext database;
        private readonly AccessControl accessControl;

        public CartModel(AppDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }


        public List<Booking> GetCartItems()
        {
            var cartItems = database.Bookings
                .Where(cart => cart.AccountID == accessControl.LoggedInAccountID)
                .Include(cart => cart.Event)
                .ToList();

            if (cartItems != null)
            {
                return cartItems;
            }
            else
            {
                return null;
            }
        }

        public void CountCartItems()
        {
            CountItems = Bookings.Sum(cart => cart.Quantity);
        }

        public double CalculateTotalPrice()
        {
            TotalPrice = Bookings.Sum(cart => cart.Event.Price * cart.Quantity);
            return TotalPrice;
        }

        public ActionResult OnPostClearCart()
        {
            var cartItems = database.Bookings
                .Where(c => c.AccountID == accessControl.LoggedInAccountID)
                .ToList();

            if (cartItems != null && cartItems.Any())
            {
                database.Bookings.RemoveRange(cartItems);
                database.SaveChanges();
            }

            return RedirectToPage("/Index");
        }

        public void OnGet()
        {
            Bookings = GetCartItems();

            if (Bookings != null && Bookings.Any())
            {
                CalculateTotalPrice();
                CountCartItems();
                database.SaveChanges();
            }
            else
            {
                emptyCart = "Din varukorg är tom!";
            }
        }

        public IActionResult OnPost() //Slussar till PlaceOrder
        {
            Bookings = GetCartItems();

            if (Bookings != null && Bookings.Any())
            {
                double totalPrice = CalculateTotalPrice();

                OnPostClearCart(); 

                return RedirectToPage("OrderConfirmation", new { totalPrice = totalPrice });
            }
            else
            {
                return RedirectToPage("/Cart");
            }
        }
    }
}