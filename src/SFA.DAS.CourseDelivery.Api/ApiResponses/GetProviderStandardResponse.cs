using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetProviderStandardResponse
    {
        public string Email { get ; set ; }
        public string ContactUrl { get ; set ; }
        public string Phone { get ; set ; }

        public int StandardId { get ; set ; }

        public static implicit operator GetProviderStandardResponse(ProviderStandard source)
        {
            return new GetProviderStandardResponse
            {
                StandardId = source.StandardId,
                Email = source.Email,
                Phone = source.Phone,
                ContactUrl = source.ContactUrl
            };
        }
    }
}