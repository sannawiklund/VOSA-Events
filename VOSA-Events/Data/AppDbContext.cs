using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using VOSA_Events.Models;

namespace VOSA_Events.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
