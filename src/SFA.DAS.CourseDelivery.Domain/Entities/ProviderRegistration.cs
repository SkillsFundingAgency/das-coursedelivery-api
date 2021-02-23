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
                ProviderTypeId = source.ProviderTypeId,
                Address1 = source.Address1,
                Address2 = source.Address2,
                Address3 = source.Address3,
                Address4 = source.Address4,
                Town = source.Town,
                Postcode = source.Postcode,
                Lat = source.Lat,
                Long = source.Long,
                LegalName = source.LegalName
            };
        }
    }
}