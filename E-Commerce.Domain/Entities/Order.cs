using E_Commerce.Domain.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Domain.Entities
{
    public class Order : IEntity
    {
        public Guid Id { get; set; }
        public DateTime? Date { get; set; }
        public string? Status { get; set; }

        public int? TrackNumber { get; set; }
        public string? ShippingAddress { get; set; }
        public string? ShippingMethod { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? DeliverDate { get; set; }
        public double ShippingCost { get; set; }
        public Guid? CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; }
        public virtual ICollection<OrderItems>? orderItems { get; set; } = new List<OrderItems>();

    }
}
