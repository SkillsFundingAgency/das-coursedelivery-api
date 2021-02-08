using System;

namespace SFA.DAS.CourseDelivery.Domain.Entities
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
        public DateTime CreatedDate { get; set; }
    }
}