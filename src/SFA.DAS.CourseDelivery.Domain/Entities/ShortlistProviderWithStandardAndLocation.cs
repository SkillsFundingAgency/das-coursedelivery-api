using System;

namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ShortlistProviderWithStandardAndLocation : ProviderWithStandardLocationBase
    {
        public Guid ShortlistUserId { get; set; }
        public int StandardId { get; set; }
        public string LocationDescription { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}