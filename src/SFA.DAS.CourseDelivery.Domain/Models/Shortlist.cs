using System;

namespace SFA.DAS.CourseDelivery.Domain.Models
{
    public class Shortlist
    {
        public Guid Id { get; set; }
        public Guid ShortlistUserId { get; set; }
        public int ProviderUkprn { get; set; }
        public int CourseId { get; set; }
        public int? LocationId { get; set; }

        public static implicit operator Shortlist(Entities.Shortlist source)
        {
            return new Shortlist
            {
                Id = source.Id,
                ShortlistUserId = source.ShortlistUserId,
                ProviderUkprn = source.ProviderUkprn,
                CourseId = source.CourseId,
                LocationId = source.LocationId
            };
        }
    }
}