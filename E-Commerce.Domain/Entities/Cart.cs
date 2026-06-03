using E_Commerce.Domain.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_Commerce.Domain.Entities
{
    public class Cart : IEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }

        [JsonIgnore]
        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; }
        [JsonIgnore]
        public virtual ICollection<CartItems>? CartItems { get; set; } = new List<CartItems>();
    }
}
