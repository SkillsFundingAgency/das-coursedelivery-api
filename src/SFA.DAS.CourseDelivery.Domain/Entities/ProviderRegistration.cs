using System.Collections.Generic;

namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderRegistration : ProviderRegistrationBase
    {
        public virtual Provider Provider { get ; set ; }
        public virtual IEnumerable<ProviderRegistrationFeedbackAttribute> ProviderRegistrationFeedbackAttributes { get ; set ; }
        public virtual IEnumerable<ProviderRegistrationFeedbackRating> ProviderRegistrationFeedbackRating { get ; set ; }
    }
}