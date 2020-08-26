using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Models
{
    public class DeliveryType
    {
        public int LocationId { get; set; }
        public string DeliveryModes { get; set; }
        public double DistanceInMiles { get; set; }

        public static implicit operator DeliveryType(ProviderWithStandardAndLocation source)
        {
            return new DeliveryType
            {
                LocationId = source.LocationId,
                DeliveryModes = source.DeliveryModes,
                DistanceInMiles = source.DistanceInMiles
            };
        }
    }
}