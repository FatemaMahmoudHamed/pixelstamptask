using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PixelStamp.Infrastructure;
using PixelStamp.Infrastructure.DbContexts;
using Serilog;
using System;

namespace PixelStamp.Portal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var configuration = new ConfigurationBuilder()
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();


            try
            {
                // Initialize the database.
                var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();

                using (var scope = scopeFactory.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<CommandDbContext>();

                    if (db.Database.EnsureCreated())
                    {
                        Log.Information("Initializing Database.");
                        SeedData.Initialize(db, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development);
                        Log.Information("Database initialized successfully.");
                    }
                
                }

                Log.Information("pixel stamp is Starting Up.");

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "pixel stamp failed to start.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


    }
}
