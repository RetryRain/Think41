using Microsoft.EntityFrameworkCore;
using Think41.Models;

namespace Think41.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set primary keys (if not using [Key] attribute)
            modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            modelBuilder.Entity<Order>().HasKey(o => o.Order_Id);

            // Define relationship: Customer has many Orders
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.User_Id)
                .OnDelete(DeleteBehavior.Cascade); // optional
        }
    }
}
