namespace E_Commerce.Application.DTOs.OrderDto
{
    public class OrderBase
    {
        public DateTime? Date { get; set; }
        public string? Status { get; set; }

        public int? TrackNumber { get; set; }
        public string? ShippingAddress { get; set; }
        public string? ShippingMethod { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? DeliverDate { get; set; }
        public double ShippingCost { get; set; }

    }
}
