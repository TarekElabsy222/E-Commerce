using AutoMapper;
using E_Commerce.Application.DTOs.AccountDto;
using E_Commerce.Application.DTOs.Brand;
using E_Commerce.Application.DTOs.CartDto;
using E_Commerce.Application.DTOs.CartItemsDto;
using E_Commerce.Application.DTOs.CategoryDto;
using E_Commerce.Application.DTOs.OrderDto;
using E_Commerce.Application.DTOs.OrderItemsDto;
using E_Commerce.Application.DTOs.PayMentDto;
using E_Commerce.Application.DTOs.ProductDto;
using E_Commerce.Application.DTOs.Review;
using E_Commerce.Application.DTOs.WishList;
using E_Commerce.Application.DTOs.WishListItemsDto;
using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // Category Mapping
            CreateMap<Category, GetCategory>();
            CreateMap<CreateCategory, Category>();
            CreateMap<UpdateCategory, Category>();

            // Brand Mapping
            CreateMap<Brand, GetBrand>();
            CreateMap<CreateBrand, Brand>();
            CreateMap<UpdateBrand, Brand>();

            //Product Mapping 
            CreateMap<Product, GetProduct>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand != null ? src.Brand.Name : null));
            CreateMap<CreateProduct, Product>();
            CreateMap<UpdateProduct, Product>();

            // Account Mapping
            CreateMap<RegisterDto, ApplicationUser>().ReverseMap();
            CreateMap<UserDto, ApplicationUser>().ReverseMap();

            // Review Mapping
            CreateMap<Review, GetReview>();
            CreateMap<CreateReview, Review>();
            CreateMap<UpdateReview, Review>();

            // WishList Mapping
            CreateMap<Wishlist, GetWishlist>();
            CreateMap<CreateWishlist, Wishlist>();
            CreateMap<UpdateWishlist, Wishlist>();

            // WishListItems Mapping
            CreateMap<WishlistItems, GetWishListItems>();
            CreateMap<CreateWishListItems, WishlistItems>();
            CreateMap<UpdateWishListItems, WishlistItems>();

            // Cart Mapping
            CreateMap<Cart, GetCart>();
            CreateMap<CreateCart, Cart>();
            CreateMap<UpdateCart, Cart>();

            // CartItems Mapping 
            CreateMap<CartItems, GetCartItems>();
            CreateMap<CreateCartItems, CartItems>();
            CreateMap<UpdateCartItems, CartItems>();

            // Order Mapping 
            CreateMap<Order, GetOrder>();
            CreateMap<CreateOrder, Order>();
            CreateMap<UpdateOrder, Order>();

            // OrderItems Mapping 
            CreateMap<OrderItems, GetOrderItems>();
            CreateMap<CreateOrderItems, OrderItems>();
            CreateMap<UpdateOrderItems, OrderItems>();

            // Payment Mapping 
            CreateMap<Payment, GetPayment>();
            CreateMap<CreatePayment, Payment>();
            CreateMap<UpdatePayment, Payment>();
        }
    }
}
