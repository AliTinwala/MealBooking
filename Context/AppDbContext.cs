using MEAL_2024_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MEAL_2024_API.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<ContactModel> Contacts { get; set; }

        public DbSet<BookingModel> Bookings {  get; set; }

        public DbSet<CouponModel> Coupons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
        }

       
    }
}
