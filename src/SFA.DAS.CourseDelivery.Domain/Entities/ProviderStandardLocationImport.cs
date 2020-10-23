using SFA.DAS.CourseDelivery.Domain.Configuration;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;

namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderStandardLocationImport : ProviderStandardLocationBase
    {
        public ProviderStandardLocationImport Map(ImportTypes.StandardLocation location, int ukprn, int standardId, double lat, double lon)
        {
            return new ProviderStandardLocationImport
            {
                StandardId = standardId,
                Ukprn = ukprn,
                Radius = location.Radius,
                LocationId = location.Id,
                DeliveryModes = string.Join("|",location.DeliveryModes),
                National = location.DeliveryModes.Contains(Constants.NationalDeliveryMode) && lat.Equals(Constants.NationalLat) && lon.Equals(Constants.NationalLong)
            };
        }
    }
}