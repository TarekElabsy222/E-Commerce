using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace E_Commerce.Application.DTOs.Review
{
    public class GetReview : ReviewBase
    {
        public Guid Id { get; set; }       
       
    }
}
