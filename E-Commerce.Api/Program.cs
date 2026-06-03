
using E_Commerce.Application.DependencyInjection;
using E_Commerce.Infrastructure.DependencyInjection;
using Serilog;

namespace E_Commerce.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("log/log.txt", rollingInterval: RollingInterval.Day).CreateLogger();
            builder.Host.UseSerilog();
            Log.Logger.Information("Application is building ..............");

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();



            // Register custom services
            builder.Services.AddSwaggerGen();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices(builder.Configuration);


            try
            {
                var app = builder.Build();
                app.UseSerilogRequestLogging();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.MapOpenApi();
                }
                app.UseSwaggerUI();
                app.UseSwagger();
                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();

                Log.Logger.Information("Application is running ..............");

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Application failed to start ..........");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
