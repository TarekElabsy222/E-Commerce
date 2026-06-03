namespace E_Commerce.Application.DTOs
{
    public record ServiceResponse(bool Success = false , string Message = null!, object Model = null!);
}
