using E_Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using E_Commerce.Domain.Repositories.Interfaces;
using E_Commerce.Infrastructure.Repositories.Implementations;
using E_Commerce.Application.Services.Interfaces.Logging;
using E_Commerce.Infrastructure.Services;
using E_Commerce.Infrastructure.MiddleWare;
using Microsoft.AspNetCore.Builder;
using E_Commerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        //add services
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>().AddEntityFrameworkStores<AppDbContext>();
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default"),
            sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                sqlOptions.EnableRetryOnFailure();
            }).UseExceptionProcessor(),
            ServiceLifetime.Scoped);

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IAppLogger<>), typeof(SerilogLoggerAdapter<>));

            return services;
        }
        // add middleware
        public static IApplicationBuilder UseInfrastructureService(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}
