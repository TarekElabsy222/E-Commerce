using E_Commerce.Domain.Entities;

namespace E_Commerce.Domain.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Brand> Brands{ get; }
        IGenericRepository<ApplicationUser> ApplicationUsers{ get; }
        IGenericRepository<Review> Reviews{ get; }
        IGenericRepository<Wishlist> Wishlists { get; }
        IGenericRepository<WishlistItems> WishlistItems { get; }
        IGenericRepository<Cart> Carts { get; }
        IGenericRepository<CartItems> CartItems { get; }
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<OrderItems> OrderItems { get; }
        IGenericRepository<Payment> Payments { get; }
        Task<int> SaveChangesAsync();
    }
}
