using Microsoft.EntityFrameworkCore;
using Think41.Models;

namespace Think41.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set primary keys (if not using [Key] attribute)
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Order>().HasKey(o => o.Order_Id);

            // Define relationship: User has many Orders
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.User_Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
