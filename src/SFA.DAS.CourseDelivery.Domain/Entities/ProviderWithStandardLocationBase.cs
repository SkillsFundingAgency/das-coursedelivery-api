using System;

namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderWithStandardLocationBase
    {
        public int Ukprn { get; set; }
        public string Name { get; set; }
        public string TradingName { get; set; }
        public string ContactUrl { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public string Postcode { get; set; }
        public string County { get; set; }
        public int LocationId { get ; set ; }
        
        public string DeliveryModes { get; set; }
        public bool National { get; set; }
        public float DistanceInMiles { get; set; }
        public long? Id { get; set; }
        public Age? Age { get; set; }
        public string SectorSubjectArea { get; set; }
        public ApprenticeshipLevel? ApprenticeshipLevel { get; set; }
        public int? OverallCohort { get; set; }
        public decimal? OverallAchievementRate { get; set; }
        public string AttributeName { get; set; }
        public int? Strength { get; set; }
        public int? Weakness { get; set; }
        public string FeedbackName { get; set; }
        public int? FeedbackCount { get; set; }
        public float ProviderDistanceInMiles { get; set; }
        public string ProviderHeadOfficeAddress1 { get; set; }
        public string ProviderHeadOfficeAddress2 { get; set; }
        public string ProviderHeadOfficeAddress3 { get; set; }
        public string ProviderHeadOfficeAddress4 { get; set; }
        public string ProviderHeadOfficeTown { get; set; }
        public string ProviderHeadOfficePostcode { get; set; }
        public Guid? ShortlistId { get; set; } 
    }
}