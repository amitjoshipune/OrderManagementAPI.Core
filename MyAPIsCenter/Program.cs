
using YourNamespace.Middleware;
using YourNamespace.Services;

namespace MyAPIsCenter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:62505")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            builder.Services.AddMemoryCache();

            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<IBookService, BookService>();
            builder.Services.AddSingleton<ICartService, CartService>();
            builder.Services.AddSingleton<IOrderService, OrderService>();

            //builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            //app.UseRouting();

            app.UseCors("AllowSpecificOrigin");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
