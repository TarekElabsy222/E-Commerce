using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Repositories.Interfaces;
using E_Commerce.Infrastructure.Data;

namespace E_Commerce.Infrastructure.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Field
        private readonly AppDbContext _context;
        public IGenericRepository<Product> Products { get; private set; }
        public IGenericRepository<Category> Categories { get; private set; }
        public IGenericRepository<Brand> Brands{ get; private set; }
        public IGenericRepository<ApplicationUser> ApplicationUsers { get; private set; }
        public IGenericRepository<Review> Reviews { get; private set; }
        public IGenericRepository<Wishlist> Wishlists { get; private set; }
        public IGenericRepository<WishlistItems> WishlistItems { get; private set; }

        public IGenericRepository<Cart> Carts { get; private set; }

        public IGenericRepository<CartItems> CartItems { get; private set; }

        public IGenericRepository<Order> Orders { get; private set; }

        public IGenericRepository<OrderItems> OrderItems { get; private set; }
        public IGenericRepository<Payment> Payments { get; private set; }


        #endregion

        #region constructor
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Products = new GenericRepository<Product>(_context);
            Categories = new GenericRepository<Category>(_context);
            Brands = new GenericRepository<Brand>(_context);
            ApplicationUsers = new GenericRepository<ApplicationUser>(_context);
            Reviews = new GenericRepository<Review>(_context);
            Wishlists = new GenericRepository<Wishlist>(_context);
            WishlistItems = new GenericRepository<WishlistItems>(_context);
            Carts = new GenericRepository<Cart>(_context);
            CartItems = new GenericRepository<CartItems>(_context);
            Orders = new GenericRepository<Order>(_context);
            OrderItems = new GenericRepository<OrderItems>(_context);
            Payments = new GenericRepository<Payment>(_context);

        }
        #endregion

        #region Handle Functions

        public void Dispose()
        {
            _context.Dispose();
        }
        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
        #endregion

    }
}
