using E_Commerce.Domain.Repositories.Interfaces;
using System.Text.Json.Serialization;

namespace E_Commerce.Domain.Entities
{
    public class Category : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public Guid? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        //navigation property
        [JsonIgnore]
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
