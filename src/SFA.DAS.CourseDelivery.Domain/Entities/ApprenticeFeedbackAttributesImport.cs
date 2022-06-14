namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ApprenticeFeedbackAttributesImport : ApprenticeFeedbackAttributesBase
    {
        public static implicit operator ApprenticeFeedbackAttributesImport(ImportTypes.ApprenticeFeedbackAttribute source)
        {
            return new ApprenticeFeedbackAttributesImport()
            {
                AttributeId = source.AttributeId,
                AttributeName = source.AttributeName,
                Category = source.Category
            };
        }
    }
}
