using System.Collections.Generic;

namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderRegistration : ProviderRegistrationBase
    {
        public virtual Provider Provider { get ; set ; }
        public virtual IEnumerable<ProviderRegistrationFeedbackAttribute> ProviderRegistrationFeedbackAttributes { get ; set ; }
        public virtual IEnumerable<ProviderRegistrationFeedbackRating> ProviderRegistrationFeedbackRating { get ; set ; }
        
        public static implicit operator ProviderRegistration(ProviderRegistrationImport source)
        {
            return new ProviderRegistration
            {
                Ukprn = source.Ukprn,
                FeedbackTotal = source.FeedbackTotal,
                StatusDate = source.StatusDate,
                StatusId = source.StatusId,
                OrganisationTypeId = source.OrganisationTypeId,
                ProviderTypeId = source.ProviderTypeId
            };
        }
    }
}