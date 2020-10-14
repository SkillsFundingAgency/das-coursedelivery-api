using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SFA.DAS.CourseDelivery.Data;

namespace SFA.DAS.CourseDelivery.Api.AcceptanceTests.Infrastructure
{
    public class AcceptanceTestingWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                configurationBuilder.AddInMemoryCollection(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("ConfigurationStorageConnectionString", "UseDevelopmentStorage=true;"),
                    new KeyValuePair<string, string>("ConfigNames", "SFA.DAS.CourseDelivery.Api"),
                    new KeyValuePair<string, string>("Environment", "DEV"),
                    new KeyValuePair<string, string>("Version", "1.0")
                });
            });

            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .AddEntityFrameworkProxies()
                    .BuildServiceProvider();

                services.AddDbContext<CourseDeliveryReadonlyDataContext>(options =>
                {
                    options.UseInMemoryDatabase("SFA.DAS.CourseDelivery");
                    options.UseInternalServiceProvider(serviceProvider);
                    options.EnableSensitiveDataLogging();
                });
                services.AddTransient(provider => new Lazy<CourseDeliveryReadonlyDataContext>(provider.GetService<CourseDeliveryReadonlyDataContext>()));


                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<CourseDeliveryReadonlyDataContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<AcceptanceTestingWebApplicationFactory<TStartup>>>();

                    db.Database.EnsureCreated();

                    try
                    {
                        DbUtilities.LoadTestData(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the database. Error: {Message}", ex.Message);
                        throw;
                    }
                }
            });
        }
    }
}
