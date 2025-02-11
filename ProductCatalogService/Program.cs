
using CommonServicesLib.Contracts;
using CommonServicesLib.Services;

using Microsoft.EntityFrameworkCore;
using ProductCatalogService.Data;
using ProductCatalogService.Middlewares;

namespace ProductCatalogService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddMemoryCache();

            builder.Services.AddScoped<ICacheService, CacheService>();
            // Register IBookService and IProductRepository
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            // Configure DbContext
            builder.Services.AddDbContext<ProductDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            // Configure CORS
            builder.Services.AddCors(options =>
            {
                //  builder => builder.WithOrigins("http://localhost:5000", "https://localhost:5009")
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:5000", "https://localhost:5009")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
           // builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowSpecificOrigin");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
