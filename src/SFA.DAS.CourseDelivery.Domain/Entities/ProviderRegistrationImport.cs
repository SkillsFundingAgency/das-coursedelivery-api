namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderRegistrationImport : ProviderRegistrationBase
    {
        public static implicit operator ProviderRegistrationImport(ImportTypes.ProviderRegistration source)
        {
            return new ProviderRegistrationImport
            {
                Ukprn = source.Ukprn,
                StatusDate = source.StatusDate,
                StatusId = source.StatusId,
                ProviderTypeId = source.ProviderTypeId,
                OrganisationTypeId = source.OrganisationTypeId,
                FeedbackTotal = source.Feedback.Total
            };
        }
    }
}
