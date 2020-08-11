using System.Collections.Generic;

namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderStandard : ProviderStandardBase
    {
        public virtual Provider Provider { get ; set ; }
        public virtual ICollection<ProviderStandardLocation> ProviderStandardLocation { get ; set ; }
        public virtual ICollection<NationalAchievementRate> NationalAchievementRate { get ; set ; }

        public static implicit operator ProviderStandard(ProviderStandardImport providerStandardImport)
        {
            return new ProviderStandard
            {
                Email = providerStandardImport.Email,
                Phone = providerStandardImport.Phone,
                Ukprn = providerStandardImport.Ukprn,
                ContactUrl = providerStandardImport.ContactUrl,
                StandardId = providerStandardImport.StandardId,
                StandardInfoUrl = providerStandardImport.StandardInfoUrl
            };
        }
    }
}