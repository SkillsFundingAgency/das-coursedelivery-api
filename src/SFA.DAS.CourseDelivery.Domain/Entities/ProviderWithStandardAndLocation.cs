namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderWithStandardAndLocation : ProviderWithStandardLocationBase
    {
        public static implicit operator ProviderWithStandardAndLocation(ShortlistProviderWithStandardAndLocation source)
        {
            return new ProviderWithStandardAndLocation
            {
                Address1 = source.Address1,
                Address2 = source.Address2,
                Age = source.Age,
                County = source.County,
                Email = source.Email,
                StandardInfoUrl = source.StandardInfoUrl,
                Id = source.Id,
                Name = source.Name,
                National = source.National,
                Phone = source.Phone,
                Postcode = source.Postcode,
                Strength = source.Strength,
                Town = source.Town,
                Ukprn = source.Ukprn,
                Weakness = source.Weakness,
                ApprenticeshipLevel = source.ApprenticeshipLevel,
                AttributeName = source.AttributeName,
                ContactUrl = source.ContactUrl,
                DeliveryModes = source.DeliveryModes,
                FeedbackCount = source.FeedbackCount,
                FeedbackName = source.FeedbackName,
                LocationId = source.LocationId,
                OverallCohort = source.OverallCohort,
                TradingName = source.TradingName,
                DistanceInMiles = source.DistanceInMiles,
                OverallAchievementRate = source.OverallAchievementRate,
                SectorSubjectArea = source.SectorSubjectArea,
                ProviderDistanceInMiles = source.ProviderDistanceInMiles,
                ProviderHeadOfficeAddress1 = source.ProviderHeadOfficeAddress1,
                ProviderHeadOfficeAddress2 = source.ProviderHeadOfficeAddress2,
                ProviderHeadOfficeAddress3 = source.ProviderHeadOfficeAddress3,
                ProviderHeadOfficeAddress4 = source.ProviderHeadOfficeAddress4,
                ProviderHeadOfficePostcode = source.ProviderHeadOfficePostcode,
                ProviderHeadOfficeTown = source.ProviderHeadOfficeTown,
                ShortlistId = source.ShortlistId
            };
        }
    }
}