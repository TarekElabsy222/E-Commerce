namespace E_Commerce.Application.DTOs.PayMentDto
{
    public class PaymentBase
    {
        public string Currency { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
    }
}
