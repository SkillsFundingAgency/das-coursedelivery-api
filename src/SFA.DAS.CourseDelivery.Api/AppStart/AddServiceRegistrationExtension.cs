using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using SFA.DAS.CourseDelivery.Application.Provider.Services;
using SFA.DAS.CourseDelivery.Application.ProviderCourseImport.Services;
using SFA.DAS.CourseDelivery.Data.Repository;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.CourseDelivery.Infrastructure.Api;

namespace SFA.DAS.CourseDelivery.Api.AppStart
{
    public static class AddServiceRegistrationExtension
    {
        public static void AddServiceRegistration(this IServiceCollection services, bool isDev)
        {
            if (isDev)
            {
                services.AddTransient<ICourseDirectoryService, DevCourseDirectoryService>();
            }
            else
            {
                services.AddHttpClient<ICourseDirectoryService, CourseDirectoryService>
                    (
                        options=> options.Timeout = TimeSpan.FromMinutes(10)
                    )
                    .SetHandlerLifetime(TimeSpan.FromMinutes(10))
                    .AddPolicyHandler(GetCourseDirectoryRetryPolicy());    
            }
            

            services.AddTransient<IProviderCourseImportService, ProviderCourseImportService>();
            services.AddTransient<IProviderService, ProviderService>();
            
            services.AddTransient<IProviderImportRepository, ProviderImportRepository>();
            services.AddTransient<IProviderStandardImportRepository, ProviderStandardImportRepository>();
            services.AddTransient<IProviderStandardLocationImportRepository, ProviderStandardLocationImportRepository>();
            services.AddTransient<IStandardLocationImportRepository, StandardLocationImportRepository>();
            services.AddTransient<IProviderRepository, ProviderRepository>();
            services.AddTransient<IProviderStandardRepository, ProviderStandardRepository>();
            services.AddTransient<IProviderStandardLocationRepository, ProviderStandardLocationRepository>();
            services.AddTransient<IStandardLocationRepository, StandardLocationRepository>();
            services.AddTransient<IImportAuditRepository, ImportAuditRepository>();

        }
        private static IAsyncPolicy<HttpResponseMessage> GetCourseDirectoryRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.BadRequest)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                    retryAttempt)));
        }
    }
    
}