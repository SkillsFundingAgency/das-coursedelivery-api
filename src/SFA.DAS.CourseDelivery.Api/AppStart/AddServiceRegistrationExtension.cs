using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using SFA.DAS.CourseDelivery.Application.OverallNationalAchievementRates.Services;
using SFA.DAS.CourseDelivery.Application.Provider.Services;
using SFA.DAS.CourseDelivery.Application.ProviderCourseImport.Services;
using SFA.DAS.CourseDelivery.Data.Repository;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.CourseDelivery.Infrastructure.Api;
using SFA.DAS.CourseDelivery.Infrastructure.PageParsing;
using SFA.DAS.CourseDelivery.Infrastructure.StreamHelper;

namespace SFA.DAS.CourseDelivery.Api.AppStart
{
    public static class AddServiceRegistrationExtension
    {
        public static void AddServiceRegistration(this IServiceCollection services, bool isDev)
        {
            if (isDev)
            {
                services.AddTransient<ICourseDirectoryService, DevCourseDirectoryService>();
                services.AddTransient<IRoatpApiService, DevRoatpApiService>();
            }
            else
            {
                services.AddHttpClient<ICourseDirectoryService, CourseDirectoryService>
                    (
                        options=> options.Timeout = TimeSpan.FromMinutes(10)
                    )
                    .SetHandlerLifetime(TimeSpan.FromMinutes(10))
                    .AddPolicyHandler(HttpClientRetryPolicy());
                services.AddTransient<IRoatpApiService, RoatpApiService>();
            }

            services.AddHttpClient<IDataDownloadService, DataDownloadService>();
            
            services.AddTransient<IProviderCourseImportService, ProviderCourseImportService>();
            services.AddTransient<IProviderService, ProviderService>();
            services.AddTransient<INationalAchievementRatesPageParser, NationalAchievementRatesPageParser>();
            services.AddTransient<IZipArchiveHelper, ZipArchiveHelper>();
            services.AddTransient<INationalAchievementRatesImportService, NationalAchievementRatesImportService>();
            services.AddTransient<INationalAchievementRatesOverallImportService, NationalAchievementRatesOverallImportService>();
            services.AddTransient<IOverallNationalAchievementRateService, OverallNationalAchievementRateService>();

            services.AddTransient<IProviderImportRepository, ProviderImportRepository>();
            services.AddTransient<IProviderStandardImportRepository, ProviderStandardImportRepository>();
            services.AddTransient<IProviderStandardLocationImportRepository, ProviderStandardLocationImportRepository>();
            services.AddTransient<IStandardLocationImportRepository, StandardLocationImportRepository>();
            services.AddTransient<IProviderRepository, ProviderRepository>();
            services.AddTransient<IProviderStandardRepository, ProviderStandardRepository>();
            services.AddTransient<IProviderStandardLocationRepository, ProviderStandardLocationRepository>();
            services.AddTransient<IStandardLocationRepository, StandardLocationRepository>();
            services.AddTransient<IImportAuditRepository, ImportAuditRepository>();
            services.AddTransient<INationalAchievementRateImportRepository, NationalAchievementRateImportRepository>();
            services.AddTransient<INationalAchievementRateRepository, NationalAchievementRateRepository>();
            services.AddTransient<INationalAchievementRateOverallRepository, NationalAchievementRateOverallRepository>();
            services.AddTransient<INationalAchievementRateOverallImportRepository, NationalAchievementRateOverallImportRepository>();
            services.AddTransient<IProviderRegistrationImportRepository, ProviderRegistrationImportRepository>();
            services.AddTransient<IProviderRegistrationRepository, ProviderRegistrationRepository>();
            services.AddTransient<IProviderRegistrationImportService, ProviderRegistrationImportService>();
            services.AddTransient<IAzureClientCredentialHelper, AzureClientCredentialHelper>();
        }
        private static IAsyncPolicy<HttpResponseMessage> HttpClientRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.BadRequest)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                    retryAttempt)));
        }
    }
}
