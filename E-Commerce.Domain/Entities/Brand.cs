using E_Commerce.Domain.Repositories.Interfaces;
using System.Text.Json.Serialization;

namespace E_Commerce.Domain.Entities
{
    public class Brand : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        //navigation property
        [JsonIgnore]
        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}
