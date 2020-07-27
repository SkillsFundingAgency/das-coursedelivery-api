using System.Collections.Generic;

namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderStandard : ProviderStandardBase
    {
        public virtual Provider Provider { get ; set ; }
        public virtual ICollection<ProviderStandardLocation> ProviderStandardLocation { get ; set ; }
    }
}