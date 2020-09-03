using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderWithStandardAndLocation
    {
        public int Ukprn { get; set; }
        public string Name { get; set; }
        public int LocationId { get ; set ; }
        public string DeliveryModes { get; set; }
        public float DistanceInMiles { get; set; }
        public long? Id { get; set; }
        public Age? Age { get; set; }
        public string SectorSubjectArea { get; set; }
        public ApprenticeshipLevel? ApprenticeshipLevel { get; set; }
        public int? OverallCohort { get; set; }
        public decimal? OverallAchievementRate { get; set; }
    }

}