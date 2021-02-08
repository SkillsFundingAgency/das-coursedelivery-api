using System;

namespace SFA.DAS.CourseDelivery.Domain.Models
{
    public class Shortlist
    {
        public Guid Id { get; set; }
        public Guid ShortlistUserId { get; set; }
        public int ProviderUkprn { get; set; }
        public int CourseId { get; set; }
        public int CourseLevel { get; set; }
        public string CourseSector { get; set; }
        public string LocationDescription { get; set; }
        public float? Lat { get; set; }
        public float? Long { get; set; }
        public ProviderLocation ProviderLocation { get; set; }

        public static implicit operator Shortlist(Entities.Shortlist source)
        {
            return new Shortlist
            {
                Id = source.Id,
                ShortlistUserId = source.ShortlistUserId,
                ProviderUkprn = source.ProviderUkprn,
                CourseId = source.CourseId,
                CourseLevel = source.CourseLevel,
                CourseSector = source.CourseSector,
                LocationDescription = source.LocationDescription,
                Lat = source.Lat,
                Long = source.Long
            };
        }
    }
}