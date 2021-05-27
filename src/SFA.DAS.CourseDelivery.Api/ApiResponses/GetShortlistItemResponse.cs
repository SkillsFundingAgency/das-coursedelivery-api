using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetShortlistItemResponse
    {
        public Guid Id { get; set; }
        public Guid ShortlistUserId { get; set; }
        public GetProviderDetailResponse ProviderDetails { get; set; }
        public int CourseId { get; set; }
        public string LocationDescription { get; set; }
        public DateTime CreatedDate { get; set; }

        public static implicit operator GetShortlistItemResponse(Shortlist source)
        {
            if (!string.IsNullOrEmpty(source.LocationDescription))
            {
                if (source.ProviderLocation.DeliveryTypes.Count(c => c.DistanceInMiles <= (double) c.Radius) == 0)
                {
                    source.ProviderLocation.DeliveryTypes = new List<DeliveryType>
                    {
                        new DeliveryType
                        {
                            DeliveryModes = "NotFound"
                        }
                    };
                }
                else
                {
                    source.ProviderLocation.DeliveryTypes = source.ProviderLocation.DeliveryTypes
                        .Where(c => c.DistanceInMiles <= (double) c.Radius).ToList();
                }
            }
            
            return new GetShortlistItemResponse
            {
                Id = source.Id,
                ShortlistUserId = source.ShortlistUserId,
                ProviderDetails = GetProviderDetailResponse.Map(source.ProviderLocation),
                CourseId = source.StandardId,
                LocationDescription = source.LocationDescription,
                CreatedDate = source.CreatedDate
            };
        }
    }
}