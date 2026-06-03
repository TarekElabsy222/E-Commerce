namespace E_Commerce.Application.DTOs.CartItemsDto
{
    public class CartItemsBase
    {
        public int Quantity { get; set; }
        public Guid? CartId { get; set; }
        public Guid? ProductId { get; set; }

    }
}
