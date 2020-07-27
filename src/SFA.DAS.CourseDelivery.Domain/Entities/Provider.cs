using System.Collections.Generic;

namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class Provider : ProviderBase
    {
        public virtual ICollection<ProviderStandard> ProviderStandards { get ; set ; }
    }
}