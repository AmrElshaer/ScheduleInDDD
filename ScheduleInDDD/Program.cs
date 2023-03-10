using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using ScheduleInDDD;
using ScheduleInDDD.Infrastructure.Data;

public class Program
{
    public static async System.Threading.Tasks.Task Main(string[] args)
    {
        var host = CreateHostBuilder(args)
                    .Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var hostEnvironment = services.GetService<IWebHostEnvironment>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogInformation($"Starting in environment {hostEnvironment.EnvironmentName}");
            try
            {
                var seedService = services.GetRequiredService<AppDbContextSeed>();
                //var catalogContext = services.GetRequiredService<AppDbContext>();
                await seedService.SeedAsync(new OfficeSettings().TestDate);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the DB.");
            }
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
          .UseServiceProviderFactory(new AutofacServiceProviderFactory())
          .ConfigureWebHostDefaults(webBuilder =>
          {
              webBuilder.UseStartup<Startup>();
          });
}