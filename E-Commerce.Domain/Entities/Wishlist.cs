using E_Commerce.Domain.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_Commerce.Domain.Entities
{
    public class Wishlist : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CustomerId { get; set; }

        //Navigation Property
        [JsonIgnore]
        [ForeignKey(nameof(CustomerId))]
        public virtual ApplicationUser Customer { get; set; }
        [JsonIgnore]
        public virtual ICollection<WishlistItems>? wishlistItems { get; set; } = new List<WishlistItems>();
    }
}
