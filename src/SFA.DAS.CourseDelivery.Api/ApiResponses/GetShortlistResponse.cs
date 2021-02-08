using System;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetShortlistResponse
    {
        public Guid Id { get; set; }
        public Guid ShortlistUserId { get; set; }
        public int ProviderUkprn { get; set; }
        public int CourseId { get; set; }

        public static implicit operator GetShortlistResponse(Shortlist source)
        {
            return new GetShortlistResponse
            {
                Id = source.Id,
                ShortlistUserId = source.ShortlistUserId,
                ProviderUkprn = source.ProviderUkprn,
                CourseId = source.CourseId
            };
        }
    }
}