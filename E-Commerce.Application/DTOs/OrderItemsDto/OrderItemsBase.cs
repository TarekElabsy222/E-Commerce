using AutoMapper.Configuration.Annotations;

namespace E_Commerce.Application.DTOs.OrderItemsDto
{
    public class OrderItemsBase
    {
        public int Quantity { get; set; }
        [Ignore]
        public double TotalPrice { get; set; }
        public double? Discount { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
    }
}
