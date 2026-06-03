using E_Commerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace E_Commerce.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
            .HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Brand> brands { get; set; }
        public DbSet<Wishlist> wishlists{ get; set; }
        public DbSet<WishlistItems> wishlistItems { get; set; }
        public DbSet<Review> reviews { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<CartItems> cartItems { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItems> orderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }

    }
}
