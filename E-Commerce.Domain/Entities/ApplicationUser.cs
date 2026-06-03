using E_Commerce.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace E_Commerce.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid> , IEntity
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? address { get; set; }

        //Navigation Property
        public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();
        [JsonIgnore]
        public virtual ICollection<Cart>? Carts { get; set; } = new List<Cart>();
        public virtual ICollection<Wishlist>? Wishlists { get; set; } = new List<Wishlist>();
        public virtual ICollection<Review>? Reviews { get; set; } = new List<Review>();
        public virtual ICollection<RefreshToken>? RefreshTokens { get; set; } = new List<RefreshToken>();
        public virtual ICollection<Payment>? Payments { get; set; } = new List<Payment>();



    }
}
