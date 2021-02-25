using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.CourseDelivery.Application.Provider.Queries.GetUkprnsByCourseAndLocation;
using SFA.DAS.CourseDelivery.Application.Shortlist.Commands.CreateShortlistItemForUser;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Api.AppStart
{
    public static class MediatRExtensions
    {
        public static void AddMediatRValidation(this IServiceCollection services)
        {
            services.AddScoped(typeof(IValidator<GetUkprnsQuery>), typeof(GetUkprnsQueryValidator));
            services.AddScoped(typeof(IValidator<CreateShortlistItemForUserRequest>), typeof(CreateShortlistItemForUserValidator));
        }
    }
}