using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using VOSA_Events.Models;

namespace VOSA_Events.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Watch> Watches { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
            .HasOne(e => e.Category)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.AdminAccount)
                .WithMany(a => a.AdministeredEvents)
                .HasForeignKey(e => e.AdminAccountID);

            modelBuilder.Entity<Booking>()
                .HasOne(t => t.Account)
            .WithMany(a => a.Bookings)
                .HasForeignKey(t => t.AccountID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(t => t.Event)
                .WithMany(e => e.Bookings)
                .HasForeignKey(t => t.EventID);
        }
    }
}
