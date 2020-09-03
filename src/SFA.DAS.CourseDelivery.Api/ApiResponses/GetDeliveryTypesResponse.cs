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
                DeliveryModes = source.DeliveryModes
            };
        }

        public string DeliveryModes { get ; set ; }

        public double DistanceInMiles { get ; set ; }

        public int LocationId { get ; set ; }
    }
}