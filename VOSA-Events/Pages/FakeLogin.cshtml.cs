using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using VOSA_Events.Data;

namespace VOSA_Events.Pages
{
    public class FakeLoginModel : PageModel
    {
        private readonly AppDbContext database;
        private readonly IWebHostEnvironment environment;

        public FakeLoginModel(AppDbContext database, IWebHostEnvironment environment)
        {
            this.database = database;
            this.environment = environment;
        }

        // Log in as the user with the specified account ID, without providing a password etc.
        // IMPORTANT: This should only be allowed in development, hence the if statement below.
        public async Task<IActionResult> OnPost(int accountID)
        {
            if (!environment.IsDevelopment())
            {
                return Forbid();
            }

            // Find the account in the database.
            var account = database.Accounts.Find(accountID);
            string subject = account.OpenIDSubject;
            string issuer = account.OpenIDIssuer;
            string name = account.Name;

            // Create the fake identity and principal objects needed to fake a login.
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, subject, valueType: null, subject: identity, issuer: issuer, originalIssuer: null));
            identity.AddClaim(new Claim(ClaimTypes.Name, name, valueType: null, subject: identity, issuer: issuer, originalIssuer: null));
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);

            return RedirectToPage("./Index");
        }
    }
}
