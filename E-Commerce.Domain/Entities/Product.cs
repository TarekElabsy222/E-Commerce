using E_Commerce.Domain.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_Commerce.Domain.Entities
{
    public class Product : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockAmount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public string? Image { get; set; }
        public decimal AfterDiscount => DiscountPercentage.HasValue ? Price - (Price * DiscountPercentage.Value / 100): Price;
        //navigation property
        public Guid? CategoryId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(CategoryId))]
        public virtual Category? Category { get; set; }

        public Guid? BrandId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(BrandId))]
        public virtual Brand? Brand { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderItems>? orderItems { get; set; } = new List<OrderItems>();
        [JsonIgnore]
        public virtual ICollection<WishlistItems>? wishlistItems { get; set; } = new List<WishlistItems>();
        [JsonIgnore]
        public virtual ICollection<CartItems>? cartItems { get; set; } = new List<CartItems>();
        [JsonIgnore]
        public virtual ICollection<Review>? Reviews { get; set; } = new List<Review>();

    }
}
