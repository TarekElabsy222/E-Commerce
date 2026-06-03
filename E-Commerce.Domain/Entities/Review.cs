using E_Commerce.Domain.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_Commerce.Domain.Entities
{
    public class Review : IEntity
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public int Rate { get; set; }
        [MaxLength(250)]
        public string? Comment { get; set; }
        [JsonIgnore]
        public DateTime Date { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public Guid? CustomerId { get; set; }
        public Guid? ProductId { get; set; }
        //Navigation Property
        [JsonIgnore]
        [ForeignKey(nameof(CustomerId))]
        public virtual ApplicationUser? Customer { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(ProductId))]
        public virtual Product? Product { get; set; }

    }
}
