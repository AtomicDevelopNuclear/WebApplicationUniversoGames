using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationUniversoGames.Models;


namespace WebApplicationUniversoGames.Data
{
    public class DataContext:IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
            .Entity<News>()
            .Property(e => e.Category)
            .HasConversion<string>();
            modelBuilder
            .Entity<Review>()
            .Property(e => e.Category)
            .HasConversion<string>();
        }

        public DbSet<News> News { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
