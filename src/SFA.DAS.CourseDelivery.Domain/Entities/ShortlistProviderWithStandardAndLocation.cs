using System;

namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ShortlistProviderWithStandardAndLocation : ProviderWithStandardLocationBase
    {
        public Guid ShortlistId { get; set; }
        public Guid ShortlistUserId { get; set; }
        public int CourseId { get; set; }
        public string LocationDescription { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}