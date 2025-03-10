
using CommonServicesLib.Contracts;
using CommonServicesLib.Services;

namespace OrderService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>builder.WithOrigins("http://localhost:5000", "https://localhost:5009")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            builder.Services.AddMemoryCache();

            builder.Services.AddSingleton<MongoDBContext>();

            builder.Services.AddSingleton<ICacheService, CacheService>();
            
            builder.Services.AddSingleton<IOrderRepository, OrderRepository> ();
            builder.Services.AddSingleton<IOrderService, OrdersService>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
               // app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowSpecificOrigin");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
