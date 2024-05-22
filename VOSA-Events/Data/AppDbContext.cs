using Microsoft.EntityFrameworkCore;
using VOSA_Events.Models;

namespace VOSA_Events.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Follow> Follows { get; set; }

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
                .HasOne(b => b.Account)
            .WithMany(a => a.Bookings)
                .HasForeignKey(b => b.AccountID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Event)
                .WithMany(e => e.Bookings)
                .HasForeignKey(b => b.EventID);

            modelBuilder.Entity<Follow>()
                .HasKey(f => new {f.EventID, f.AccountID});

            modelBuilder.Entity<Follow>()
                .HasOne(f => f.Account)
                .WithMany()
                .HasForeignKey(f => f.AccountID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Follow>()
                .HasOne(f => f.Event)
                .WithMany()
                .HasForeignKey(f => f.EventID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Review>()
               .HasKey(r => new { r.EventID, r.AccountID});

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Account)
                .WithMany()
                .HasForeignKey(r => r.AccountID)
                .OnDelete(DeleteBehavior.NoAction);
           
            modelBuilder.Entity<Review>()
             .HasOne(r => r.Event)
             .WithMany()
             .HasForeignKey(r => r.EventID)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
