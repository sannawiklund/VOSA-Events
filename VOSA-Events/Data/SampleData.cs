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
					Name = "Brad"
				});
				database.Accounts.Add(new Account
				{
					OpenIDIssuer = fakeIssuer,
					OpenIDSubject = "2222222222",
					Name = "Angelina"
				});
				database.Accounts.Add(new Account
				{
					OpenIDIssuer = fakeIssuer,
					OpenIDSubject = "3333333333",
					Name = "Will"
				});
			}

			if (!database.Events.Any())

			{
				database.Events.Add(new Event
				{
					Name = "Göteborgs Mat & Dryckesfestival",
					Description = "En festival som firar lokala mat- och dryckesproducenter från Västra Götaland. Adress: Götaplatsen 1, 412 56 Göteborg",
					Price = 150,
					City = "Göteborg",
					TicketQuantity = 500,
					Date = new DateTime(2024, 8, 10, 10, 0, 0)
				});

				database.Events.Add(new Event
				{
					Name = "Lisebergs Julmarknad",
					Description = "En julmarknad med massor av lokala delikatesser och traditionell julmat. Adress: Örgrytevägen 5, 402 22 Göteborg",
					Price = 100,
					City = "Göteborg",
					TicketQuantity = 300,
					Date = new DateTime(2024, 11, 29, 12, 0, 0)
				});

				database.Events.Add(new Event
				{
					Name = "Smaka på Skaraborg",
					Description = "En mässa som hyllar Skaraborgs unika matkultur. Adress: Stenbron, 541 45 Skövde",
					Price = 80,
					City = "Skövde",
					TicketQuantity = 200,
					Date = new DateTime(2024, 6, 15, 11, 0, 0)
				});

				database.Events.Add(new Event
				{
					Name = "Gourmetmässan Borås",
					Description = "En exklusiv mässa för finsmakare med det bästa inom gourmetmat och drycker. Adress: Sturegatan 15, 503 32 Borås",
					Price = 200,
					City = "Borås",
					TicketQuantity = 150,
					Date = new DateTime(2024, 9, 20, 9, 30, 0)
				});

				database.Events.Add(new Event
				{
					Name = "Vänersborgs Matfestival",
					Description = "En festival som samlar matälskare vid Vänerns strand och erbjuder ett brett utbud av mat och drycker. Adress: Strandgatan 1, 462 30 Vänersborg",
					Price = 120,
					City = "Vänersborg",
					TicketQuantity = 400,
					Date = new DateTime(2024, 7, 5, 10, 30, 0)
				});

				database.Events.Add(new Event
				{
					Name = "Vin & Delikatessmässa Uddevalla",
					Description = "En mässa för vinentusiaster och finsmakare med provningar av lokala delikatesser. Adress: Hamnplan, 451 55 Uddevalla",
					Price = 75,
					City = "Uddevalla",
					TicketQuantity = 250,
					Date = new DateTime(2024, 10, 12, 13, 0, 0)
				});

				database.Events.Add(new Event
				{
					Name = "Matfesten Trollhättan",
					Description = "En festlig tillställning med mat, dryck och underhållning för hela familjen. Adress: Storgatan 1, 461 30 Trollhättan",
					Price = 90,
					City = "Trollhättan",
					TicketQuantity = 350,
					Date = new DateTime(2024, 8, 25, 14, 0, 0)
				});

				database.Events.Add(new Event
				{
					Name = "Kulinariska Höstmarknaden Strömstad",
					Description = "En marknad som fylls av dofter och smaker från lokala producenter och hantverkare. Adress: Hamngatan 10, 452 30 Strömstad",
					Price = 60,
					City = "Strömstad",
					TicketQuantity = 180,
					Date = new DateTime(2024, 9, 30, 11, 30, 0)
				});

				database.Events.Add(new Event
				{
					Name = "Kafé & Chokladfestivalen Kungälv",
					Description = "En festival som hyllar kaffe och choklad i alla dess former. Adress: Torget, 442 30 Kungälv",
					Price = 50,
					City = "Kungälv",
					TicketQuantity = 200,
					Date = new DateTime(2024, 11, 10, 10, 0, 0)
				});

				database.Events.Add(new Event
				{
					Name = "Skara Sommarbuffé",
					Description = "En buffé med smaker från Skara och omnejd, med lokala råvaror i fokus. Adress: Stortorget, 532 30 Skara",
					Price = 70,
					City = "Skara",
					TicketQuantity = 300,
					Date = new DateTime(2024, 7, 20, 12, 30, 0)
				});

				database.Events.Add(new Event
				{
					Name = "Mölndals Ölfestival",
					Description = "En festival för ölälskare med ett brett urval av lokala och internationella ölsorter. Adress: Slottsmöllan, 431 37 Mölndal",
					Price = 180,
					City = "Mölndal",
					TicketQuantity = 400,
					Date = new DateTime(2024, 8, 31, 15, 0, 0)
				});
			}


			database.SaveChanges();
		}
	}
}
