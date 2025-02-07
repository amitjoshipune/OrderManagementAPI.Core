using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using CommonServicesLib.Contracts;
using CommonServicesLib.Services;
using System.Text.Json.Serialization;
using CommonServicesLib;
using Microsoft.Extensions.Configuration;

namespace ShoppingCartService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            // Register Kafka settings
            builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("Kafka"));


            builder.Services.AddSingleton<MongoDBContext>();
            builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
            // Create a method to configure AutoMapper and avoid ambiguity
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }, typeof(Program));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:5000", "https://localhost:5009")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            builder.Services.AddMemoryCache();
            builder.Services.AddScoped<ICacheService, CacheService>();

            builder.Services.AddSingleton<ICartService, CartService>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowSpecificOrigin");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
