using E_Commerce.Domain.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_Commerce.Domain.Entities
{
    public class WishlistItems : IEntity
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public Guid? WishlistId { get; set; }
        public Guid? ProductId { get; set; }

        //Navigation Property
        [JsonIgnore]
        [ForeignKey(nameof(WishlistId))]
        public virtual Wishlist? Wishlist { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(ProductId))]
        public virtual Product? Product { get; set; }

    }
}
