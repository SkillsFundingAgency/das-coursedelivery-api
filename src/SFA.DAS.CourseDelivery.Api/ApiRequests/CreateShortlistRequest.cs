using System;

namespace SFA.DAS.CourseDelivery.Api.ApiRequests
{
    public class CreateShortlistRequest
    {
        public Guid ShortlistUserId { get ; set ; }
        public float? Lat { get ; set ; }
        public float? Lon { get ; set ; }
        public int StandardId { get ; set ; }
        public string LocationDescription { get ; set ; }
        public int Ukprn { get ; set ; }
        public string SectorSubjectArea { get ; set ; }
    }
}