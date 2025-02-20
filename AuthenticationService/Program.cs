
using AuthenticationService.Data;
using AuthenticationService.Repositories;
using AuthenticationService.Services;
using CommonServicesLib.Contracts;
using CommonServicesLib.Services;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService
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
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });


            builder.Services.AddMemoryCache();
            builder.Services.AddScoped<ICacheService, CacheService>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository,UserRepository>();

            builder.Services.AddDbContext<SqlServerDbContext>(options=>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("AzureDBConnection_Users"));
            });

            builder.Services.AddScoped<IAzureServiceBusClient , AzureServiceBusClient>();
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
