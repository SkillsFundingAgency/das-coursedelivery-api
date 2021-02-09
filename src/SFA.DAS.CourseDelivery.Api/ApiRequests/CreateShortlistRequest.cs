using System;

namespace SFA.DAS.CourseDelivery.Api.ApiRequests
{
    public class CreateShortlistRequest
    {
        public Guid ShortlistUserId { get ; set ; }
        public float? Lat { get ; set ; }
        public float? Lon { get ; set ; }
        public int Level { get ; set ; }
        public int CourseId { get ; set ; }
        public string LocationDescription { get ; set ; }
        public int ProviderUkprn { get ; set ; }
        public string SectorSubjectArea { get ; set ; }
    }
}