using System;

namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class Shortlist
    {
        public Guid Id { get; set; }
        public Guid ShortlistUserId { get; set; }
        public int ProviderUkprn { get; set; }
        public int CourseId { get; set; }
        public int? LocationId { get; set; }
    }
}