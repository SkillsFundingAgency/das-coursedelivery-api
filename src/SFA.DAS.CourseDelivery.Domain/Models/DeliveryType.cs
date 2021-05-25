using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Models
{
    public class DeliveryType
    {
        public int LocationId { get; set; }
        public string DeliveryModes { get; set; }
        public double DistanceInMiles { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public string Postcode { get; set; }
        public string County { get; set; }
        public bool National { get ; set ; }
        public decimal Radius { get; set; }

        public static implicit operator DeliveryType(ProviderWithStandardAndLocation source)
        {
            return new DeliveryType
            {
                LocationId = source.LocationId,
                DeliveryModes = source.DeliveryModes,
                DistanceInMiles = source.DistanceInMiles,
                Address1 = source.Address1,
                Address2 = source.Address2,
                Town = source.Town,
                Postcode = source.Postcode,
                County = source.County,
                National = source.National,
                Radius = source.Radius
            };
        }
    }
}