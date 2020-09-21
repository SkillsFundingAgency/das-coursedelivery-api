using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetFeedbackAttributeResponse
    {
        public int Weakness { get ; set ; }
        public int Strength { get ; set ; }
        public string AttributeName { get ; set ; }

        public static implicit operator GetFeedbackAttributeResponse(ProviderFeedbackAttribute source)
        {
            return new GetFeedbackAttributeResponse
            {
                AttributeName = source.AttributeName,
                Strength = source.Strength,
                Weakness = source.Weakness
            }; 
        }
    }
}