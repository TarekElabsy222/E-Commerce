namespace E_Commerce.Application.DTOs.Review
{
    public class ReviewBase
    {
        public int Rate { get; set; }
        public string Comment { get; set; }
        public Guid ProductId { get; set; }        
    }
}
