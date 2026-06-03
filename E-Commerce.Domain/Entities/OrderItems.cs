using E_Commerce.Domain.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_Commerce.Domain.Entities
{
    public class OrderItems : IEntity
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public int Quantity { get; set; }        
        public double TotalPrice { get; set; }
        public double? Discount { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }

        [JsonIgnore]
        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }
        [JsonIgnore]
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
