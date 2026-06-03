using E_Commerce.Application.Mapping;
using E_Commerce.Application.Services.Implementations;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Stripe;

namespace E_Commerce.Application.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register Mapping
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingConfig>();
            });

            // stripe config
            StripeConfiguration.ApiKey = configuration.GetSection("Stripe:SecretKey").Get<string>();

            //Email config
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            // Register Jwt 
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            )
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        ClockSkew = TimeSpan.Zero   //when token expires it expires immediately
                    };
                }
                );

            //Register Services
           
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<IBrandServices, BrandServices>();
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<IAccountServices, AccountServices>();
            services.AddScoped<IReviewServices, ReviewServices>();
            services.AddScoped<IWishlistServices, WishlistServices>();
            services.AddScoped<IWishlistItemServices, WishlistItemServices>();
            services.AddScoped<ICartServices, CartServices>();
            services.AddScoped<ICartItemsServices, CartItemsServices>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IOrderItemsServices, OrderItemsServices>();
            services.AddScoped<IPaymentServices, PaymentServices>();

            return services;
        }
    }
}
