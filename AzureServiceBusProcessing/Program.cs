using CommonServicesLib.Contracts.Azure;
using CommonServicesLib.Services.Azure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace AzureServiceBusProcessing
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddSingleton<IServiceBusReceiver>(new ServiceBusReceiver(
                "Endpoint=sb://YOU_NAMESPAE_GOES_HERE.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Pl4jzt2Deve4SLq8a+cPhS0c/au+rk0mw+ASbBL1tGo=",
                "userscreatedqueue"));
            builder.Services.AddHostedService<ServiceBusHostedService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}
