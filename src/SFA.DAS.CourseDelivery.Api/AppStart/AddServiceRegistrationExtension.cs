using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.CourseDelivery.Infrastructure.Api;

namespace SFA.DAS.CourseDelivery.Api.AppStart
{
    public static class AddServiceRegistrationExtension
    {
        public static void AddServiceRegistration(this IServiceCollection services)
        {
            services.AddTransient<ICourseDirectoryService, CourseDirectoryService>();

        }
    }
}