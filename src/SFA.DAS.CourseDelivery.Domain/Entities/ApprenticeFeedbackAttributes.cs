using SFA.DAS.CourseDelivery.Domain.ImportTypes;

namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ApprenticeFeedbackAttributes : ApprenticeFeedbackAttributesBase
    {
        //Setup EF relationships
        
        public static implicit operator ApprenticeFeedbackAttributes(ApprenticeFeedbackAttributesImport source)
        {
            return new ApprenticeFeedbackAttributes()
            {
                AttributeId = source.AttributeId,
                AttributeName = source.AttributeName,
                Category = source.Category
            };
        }
    }
}
