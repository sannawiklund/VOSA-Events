using VOSA_Events.Models;

namespace VOSA_Events.Data
{
    public class SampleData
    {
        public static void Create(AppDbContext database)
        {
            // If there are no fake accounts, add some.
            string fakeIssuer = "https://example.com";
            if (!database.Accounts.Any(a => a.OpenIDIssuer == fakeIssuer))
            {
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "1111111111",
                    Name = "John Smith",
                    Role = "Admin"
                });
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "2222222222",
                    Name = "Emily Johnson",
                    Role = "Admin"
                });
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "3333333333",
                    Name = "Michael Williams",
                    Role = "Admin"
                });
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "4444444444",
                    Name = "Jessica Brown",
                    Role = "Admin"
                });
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "5555555555",
                    Name = "David Martinez",
                    Role = "Admin"
                });
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "6666666666",
                    Name = "Amanda Wilson",
                    Role = "User"
                });
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "7777777777",
                    Name = "Daniel Taylor",
                    Role = "User"
                });
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "8888888888",
                    Name = "Jennifer Lee",
                    Role = "User"
                });
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "9999999999",
                    Name = "Christopher Clark",
                    Role = "User"
                });
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "1010101010",
                    Name = "Sarah Rodriguez",
                    Role = "User"
                });

            }
            database.SaveChanges();

            if (!database.Categories.Any())

            {
                database.Categories.Add(new Category
                {
                    Name = "Mat & Dryck",

                });

                database.Categories.Add(new Category
                {
                    Name = "Mat",
                });

                database.Categories.Add(new Category
                {
                    Name = "Dryck"
                });
            }

            database.SaveChanges();


            if (!database.Events.Any())

            {
                database.Events.Add(new Event
                {
                    Name = "Göteborgs Mat & Dryckesfestival",
                    Description = "En festival som firar lokala mat- och dryckesproducenter från Västra Götaland.",
                    Address = "Götaplatsen 1, 412 56 Göteborg",
                    Price = 150,
                    City = "Göteborg",
                    TicketQuantity = 500,
                    Date = new DateTime(2024, 8, 10, 10, 0, 0),
                    AdminAccountID = 2,
                    CategoryID = 1
                });

                database.Events.Add(new Event
                {
                    Name = "Lisebergs Julmarknad",
                    Description = "En julmarknad med massor av lokala delikatesser och traditionell julmat.",
                    Address = "Örgrytevägen 5, 402 22 Göteborg",
                    Price = 100,
                    City = "Göteborg",
                    TicketQuantity = 300,
                    Date = new DateTime(2024, 11, 29, 12, 0, 0),
                    AdminAccountID = 2,
                    CategoryID = 1

                });

                database.Events.Add(new Event
                {
                    Name = "Smaka på Skaraborg",
                    Description = "En mässa som hyllar Skaraborgs unika matkultur.",
                    Address = "Stenbron, 541 45 Skövde",
                    Price = 80,
                    City = "Skövde",
                    TicketQuantity = 200,
                    Date = new DateTime(2024, 6, 15, 11, 0, 0),
                    AdminAccountID = 3,
                    CategoryID = 2
                });

                database.Events.Add(new Event
                {
                    Name = "Gourmetmässan Borås",
                    Description = "En exklusiv mässa för finsmakare med det bästa inom gourmetmat och drycker.",
                    Address = "Sturegatan 15, 503 32 Borås",
                    Price = 200,
                    City = "Borås",
                    TicketQuantity = 150,
                    Date = new DateTime(2024, 9, 20, 9, 30, 0),
                    AdminAccountID = 4,
                    CategoryID = 1
                });

                database.Events.Add(new Event
                {
                    Name = "Vänersborgs Matfestival",
                    Description = "En festival som samlar matälskare vid Vänerns strand och erbjuder ett brett utbud av mat och drycker.",
                    Address = "Strandgatan 1, 462 30 Vänersborg",
                    Price = 120,
                    City = "Vänersborg",
                    TicketQuantity = 400,
                    Date = new DateTime(2024, 7, 5, 10, 30, 0),
                    AdminAccountID = 5,
                    CategoryID = 1
                });

                database.Events.Add(new Event
                {
                    Name = "Vin & Delikatessmässa Uddevalla",
                    Description = "En mässa för vinentusiaster och finsmakare med provningar av lokala delikatesser.",
                    Address = "Hamnplan, 451 55 Uddevalla",
                    Price = 75,
                    City = "Uddevalla",
                    TicketQuantity = 250,
                    Date = new DateTime(2024, 10, 12, 13, 0, 0),
                    AdminAccountID = 1,
                    CategoryID = 1
                });

                database.Events.Add(new Event
                {
                    Name = "Matfesten Trollhättan",
                    Description = "En festlig tillställning med mat, dryck och underhållning för hela familjen.",
                    Address = "Storgatan 1, 461 30 Trollhättan",
                    Price = 90,
                    City = "Trollhättan",
                    TicketQuantity = 350,
                    Date = new DateTime(2024, 8, 25, 14, 0, 0),
                    AdminAccountID = 2,
                    CategoryID = 1

                });

                database.Events.Add(new Event
                {
                    Name = "Kulinariska Höstmarknaden Strömstad",
                    Description = "En marknad som fylls av dofter och smaker från lokala producenter och hantverkare.",
                    Address = "Hamngatan 10, 452 30 Strömstad",
                    Price = 60,
                    City = "Strömstad",
                    TicketQuantity = 180,
                    Date = new DateTime(2024, 9, 30, 11, 30, 0),
                    AdminAccountID = 3,
                    CategoryID = 2
                });

                database.Events.Add(new Event
                {
                    Name = "Kafé & Chokladfestivalen Kungälv",
                    Description = "En festival som hyllar kaffe och choklad i alla dess former.",
                    Address = "Torget, 442 30 Kungälv",
                    Price = 50,
                    City = "Kungälv",
                    TicketQuantity = 200,
                    Date = new DateTime(2024, 11, 10, 10, 0, 0),
                    AdminAccountID = 4,
                    CategoryID = 1
                });

                database.Events.Add(new Event
                {
                    Name = "Skara Sommarbuffé",
                    Description = "En buffé med smaker från Skara och omnejd, med lokala råvaror i fokus.",
                    Address = "Stortorget, 532 30 Skara",
                    Price = 70,
                    City = "Skara",
                    TicketQuantity = 300,
                    Date = new DateTime(2024, 7, 20, 12, 30, 0),
                    AdminAccountID = 5,
                    CategoryID = 2
                });

                database.Events.Add(new Event
                {
                    Name = "Göteborgs Ölfestival",
                    Description = "En festival för ölälskare med ett brett urval av lokala och internationella ölsorter.",
                    Address = "Slottsmöllan, 431 37 Göteborg",
                    Price = 180,
                    City = "Göteborg",
                    TicketQuantity = 400,
                    Date = new DateTime(2024, 8, 31, 15, 0, 0),
                    AdminAccountID = 1,
                    CategoryID = 3
                });

                database.Events.Add(new Event
                {
                    Name = "Borås Vin & Drinkmässa",
                    Description = "En mässa för vin- och drinkentusiaster med provningar av exklusiva viner och innovativa drinkar.",
                    Address = "Mässans gata 20, 412 51 Göteborg",
                    Price = 200,
                    City = "Borås",
                    TicketQuantity = 400,
                    Date = new DateTime(2024, 10, 5, 17, 0, 0),
                    AdminAccountID = 4,
                    CategoryID = 3
                });
            }

            database.SaveChanges();

        }
    }
}