using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetDeliveryTypesResponse
    {
        public static implicit operator GetDeliveryTypesResponse(DeliveryType source)
        {
            return new GetDeliveryTypesResponse
            {
                LocationId = source.LocationId,
                DistanceInMiles = source.DistanceInMiles,
                DeliveryModes = source.DeliveryModes,
                Address1 = source.Address1,
                Address2 = source.Address2,
                Town = source.Town,
                Postcode = source.Postcode,
                County = source.County
            };
        }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public string Postcode { get; set; }
        public string County { get; set; }

        public string DeliveryModes { get ; set ; }

        public double DistanceInMiles { get ; set ; }

        public int LocationId { get ; set ; }
    }
}