using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetCourseProviderResponse
    {
        public string Name { get ; set ; }
        public int Ukprn { get ; set ; }
        public string ContactUrl { get ; set ; }
        public string Email { get ; set ; }
        public string Phone { get ; set ; }

        public static implicit operator GetCourseProviderResponse(ProviderStandard source)
        {
            return new GetCourseProviderResponse
            {
                Email = source.Email,
                Phone = source.Phone,
                ContactUrl = source.ContactUrl,
                Name = source.Provider.Name,
                Ukprn = source.Provider.Ukprn
            };
        }
    }
}