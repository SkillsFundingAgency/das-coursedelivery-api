using System;
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