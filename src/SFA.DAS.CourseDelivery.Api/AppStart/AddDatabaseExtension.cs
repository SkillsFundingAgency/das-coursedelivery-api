using System;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.CourseDelivery.Data;
using SFA.DAS.CourseDelivery.Domain.Configuration;

namespace SFA.DAS.CourseDelivery.Api.AppStart
{
    public static class AddDatabaseExtension
    {
        public static void AddDatabaseRegistration(this IServiceCollection services, CourseDeliveryConfiguration config, string environmentName)
        {
            if (environmentName.Equals("DEV", StringComparison.CurrentCultureIgnoreCase))
            {
                services.AddDbContext<CourseDeliveryDataContext>(options => options.UseInMemoryDatabase("SFA.DAS.CourseDelivery"), ServiceLifetime.Transient);
                services.AddDbContext<CourseDeliveryReadonlyDataContext>(options => options.UseInMemoryDatabase("SFA.DAS.CourseDelivery"), ServiceLifetime.Transient);
            }
            else if (environmentName.Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase))
            {
                services.AddDbContext<CourseDeliveryDataContext>(options=>options.UseSqlServer(config.ConnectionString).EnableSensitiveDataLogging(),ServiceLifetime.Transient);
                services.AddDbContext<CourseDeliveryReadonlyDataContext>(options=>options.UseSqlServer(config.ConnectionString).EnableSensitiveDataLogging(),ServiceLifetime.Transient);
            }
            else
            {
                services.AddSingleton(new AzureServiceTokenProvider());
                services.AddDbContext<CourseDeliveryDataContext>(ServiceLifetime.Transient);    
                services.AddDbContext<CourseDeliveryReadonlyDataContext>(ServiceLifetime.Transient);    
            }
            
            services.AddTransient<ICourseDeliveryDataContext, CourseDeliveryDataContext>(provider => provider.GetService<CourseDeliveryDataContext>());
            services.AddTransient<ICourseDeliveryReadonlyDataContext, CourseDeliveryReadonlyDataContext>(provider => provider.GetService<CourseDeliveryReadonlyDataContext>());
            services.AddTransient(provider => new Lazy<CourseDeliveryDataContext>(provider.GetService<CourseDeliveryDataContext>()));
            services.AddTransient(provider => new Lazy<CourseDeliveryReadonlyDataContext>(provider.GetService<CourseDeliveryReadonlyDataContext>()));
            
        }
    }
}