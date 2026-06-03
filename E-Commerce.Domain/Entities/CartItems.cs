using E_Commerce.Domain.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_Commerce.Domain.Entities
{
    public class CartItems : IEntity
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Guid? CartId { get; set; }
        public Guid? ProductId { get; set; }

        [JsonIgnore]
        [ForeignKey("CartId")]
        public virtual Cart? Cart { get; set; }

        [JsonIgnore]
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
