using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Claims;
using VOSA_Events.Data;
using VOSA_Events.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "Google";
})
.AddCookie(options =>
{
    // When a user logs in to Google for the first time, create a local account for that user in our database.
    options.Events.OnValidatePrincipal += async context =>
    {
        var serviceProvider = context.HttpContext.RequestServices;
        using var db = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

        string subject = context.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
        string issuer = context.Principal.FindFirst(ClaimTypes.NameIdentifier).Issuer;
        string name = context.Principal.FindFirst(ClaimTypes.Name).Value;
        string role = context.Principal.FindFirst(ClaimTypes.Role).Value;

        var account = db.Accounts
            .FirstOrDefault(p => p.OpenIDIssuer == issuer && p.OpenIDSubject == subject);

        if (account == null)
        {
            account = new Account
            {
                OpenIDIssuer = issuer,
                OpenIDSubject = subject,
                Name = name,
                Role = role
            };
            db.Accounts.Add(account);
        }
        else
        {
            // If the account already exists, just update the name in case it has changed.
            account.Name = name;
        }

        await db.SaveChangesAsync();
    };

	options.Events.OnValidatePrincipal += async context =>
	{
		var serviceProvider = context.HttpContext.RequestServices;
		using var db = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

		string subject = context.Principal.FindFirstValue(ClaimTypes.NameIdentifier);

		// Hantera event
		var eventName = context.Principal.FindFirst("event_name")?.Value;
		if (!string.IsNullOrEmpty(eventName))
		{
			// Skapa eller uppdatera händelse
			var eventId = int.Parse(context.Principal.FindFirst("event_id").Value);
			var eventPrice = int.Parse(context.Principal.FindFirst("event_price").Value);
			var eventDescription = context.Principal.FindFirst("event_description")?.Value;
			var eventCity = context.Principal.FindFirst("event_city")?.Value;
			var eventDate = DateTime.Parse(context.Principal.FindFirst("event_date").Value);
			var eventTicketQuantity = int.Parse(context.Principal.FindFirst("event_ticket_quantity").Value);
			var eventCategoryID = int.Parse(context.Principal.FindFirst("event_category_id").Value);
			var eventAdminAccountID = int.Parse(context.Principal.FindFirst("event_admin_account_id").Value);

			var existingEvent = db.Events.FirstOrDefault(e => e.ID == eventId);

			if (existingEvent == null)
			{
				// Mässan finns inte, skapa en ny
				var newEvent = new Event
				{
					ID = eventId,
					Name = eventName,
					Price = eventPrice,
					Description = eventDescription,
					City = eventCity,
					Date = eventDate,
					TicketQuantity = eventTicketQuantity,
					CategoryID = eventCategoryID,
					AdminAccountID = eventAdminAccountID
				};

				db.Events.Add(newEvent);
			}
			else
			{
				// Mässan finns redan, uppdatera information
				existingEvent.Name = eventName;
				existingEvent.Price = eventPrice;
				existingEvent.Description = eventDescription;
				existingEvent.City = eventCity;
				existingEvent.Date = eventDate;
				existingEvent.TicketQuantity = eventTicketQuantity;
				existingEvent.CategoryID = eventCategoryID;
				existingEvent.AdminAccountID = eventAdminAccountID;
			}
		}

		await db.SaveChangesAsync();



		// Hantera kategori
		var categoryName = context.Principal.FindFirst("category_name")?.Value;
		if (!string.IsNullOrEmpty(categoryName))
		{
			// Skapa eller uppdatera kategori
			var categoryId = int.Parse(context.Principal.FindFirst("category_id").Value);

			var existingCategory = db.Categories.FirstOrDefault(c => c.ID == categoryId);

			if (existingCategory == null)
			{
				// Kategorin finns inte, skapa en ny
				var newCategory = new Category
				{
					ID = categoryId,
					Name = categoryName
				};

				db.Categories.Add(newCategory);
			}
			else
			{
				// Kategorin finns redan, uppdatera namn
				existingCategory.Name = categoryName;
			}
		}

		await db.SaveChangesAsync();
	};

})
.AddOpenIdConnect("Google", options =>
{
    options.Authority = "https://accounts.google.com";
    /*
    These two values (client ID and client secret) must be created in the Google Cloud Platform Console:
    https://support.google.com/cloud/answer/6158849?hl=en
    They must then be added to the project's "user secrets": right-click the project in Visual Studio and select "Manage User Secrets" and write the following JSON:
    {
       "Authentication": {
           "Google": {
               "ClientId": "...",
               "ClientSecret": "..."
           }
       }
    }
    */
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    options.ResponseType = OpenIdConnectResponseType.Code;
    options.CallbackPath = "/signin-oidc-google";
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("email");
    options.SaveTokens = true;
    options.GetClaimsFromUserInfoEndpoint = true;
});

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddRazorPages(options =>
{
    //Lägg till de sidor som kräver en viss behörighet för att visa. 
}).AddRazorRuntimeCompilation(); 
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AccessControl>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    SampleData.Create(context);
}

app.Run();
