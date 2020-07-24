using SFA.DAS.CourseDelivery.Domain.ImportTypes;

namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderStandardImport : ProviderStandardBase
    {
        public ProviderStandardImport Map(CourseStandard courseStandard, int ukprn)
        {
            return new ProviderStandardImport
            {
                StandardId = courseStandard.StandardCode,
                Email = courseStandard.Contact.Email,
                Phone = courseStandard.Contact.Phone,
                ContactUrl = courseStandard.Contact.ContactUsUrl,
                StandardInfoUrl = courseStandard.StandardInfoUrl,
                Ukprn = ukprn
            };
        }
    }
}