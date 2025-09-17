using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastrucutre.Persistence
{
    internal class RestaurantsDbContext : DbContext
    {
        internal DbSet<Restaurant> Restaurants { get; set; }
        internal DbSet<Dish> Dishes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MOHAMMED-WALEED;Database=RestaurantsDB;Integrated Security=True;Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Restaurant>()
                .OwnsOne(r => r.Address);

            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Dishes)
                .WithOne()
                .HasForeignKey(d => d.RestaurantId);
        }
    }
}
