using SFA.DAS.CourseDelivery.Domain.ImportTypes;

namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderStandardLocationImport : ProviderStandardLocationBase
    {
        public ProviderStandardLocationImport Map(ImportTypes.StandardLocation location, int ukprn, int standardId)
        {
            return new ProviderStandardLocationImport
            {
                StandardId = standardId,
                Ukprn = ukprn,
                Radius = location.Radius,
                LocationId = location.Id,
                DeliveryModes = string.Join("|",location.DeliveryModes) 
            };
        }
    }
}