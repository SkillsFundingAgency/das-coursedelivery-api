using System.Collections.Generic;
using System.Linq;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Models
{
    public class ProviderLocation
    {
        public ProviderLocation ()
        {
            
        }
        public ProviderLocation(int ukPrn, string name, string tradingName, 
            string contactUrl, 
            string phone, 
            string email, 
            double providerDistanceInMiles,
            string providerHeadOfficeAddress1,
            string providerHeadOfficeAddress2,
            string providerHeadOfficeAddress3,
            string providerHeadOfficeAddress4,
            string providerHeadOfficeTown,
            string providerHeadOfficePostcode,
            IReadOnlyCollection<ProviderWithStandardAndLocation> providerWithStandardAndLocations)
        {
            Ukprn = ukPrn;
            Name = name;
            TradingName = tradingName; 
            ContactUrl = contactUrl;
            Email = email;
            Phone = phone;
            Address = new ProviderHeadOfficeAddress
            {
                Address1 = providerHeadOfficeAddress1,
                Address2 = providerHeadOfficeAddress2,
                Address3 = providerHeadOfficeAddress3,
                Address4 = providerHeadOfficeAddress4,
                Town = providerHeadOfficeTown,
                Postcode = providerHeadOfficePostcode,
                DistanceInMiles = providerDistanceInMiles,
            };
            
            
            DeliveryTypes = providerWithStandardAndLocations.GroupBy(x=>new {x.DeliveryModes, x.LocationId,x.DistanceInMiles, x.National})
                .Select(p=>p.FirstOrDefault())
                .Select(c => (DeliveryType) c).ToList();
            AchievementRates = providerWithStandardAndLocations
                .Where(c=>c.Id!=null)
                .GroupBy(x=>new {x.Id})
                .Select(p=>p.FirstOrDefault())
                .Select(c => (AchievementRate) c).ToList();
            FeedbackRating = providerWithStandardAndLocations
                .Where(c => c.FeedbackCount.HasValue)
                .GroupBy(x => x.FeedbackName)
                .Select(c => c.FirstOrDefault())
                .Select(c => (ProviderFeedbackRating) c)
                .Where(c=>c!=null)
                .ToList();
            FeedbackAttributes = providerWithStandardAndLocations
                .Where(c => !string.IsNullOrEmpty(c.AttributeName))
                .GroupBy(c => c.AttributeName)
                .Select(c => c.FirstOrDefault())
                .Select(c => (ProviderFeedbackAttribute) c)
                .Where(c => c != null)
                .ToList();
        }

        public ProviderLocation(Provider provider)
        {
            Ukprn = provider.Ukprn;
            Name = provider.Name;
            TradingName = provider.TradingName;
            AchievementRates = provider.NationalAchievementRates.Select(c => (AchievementRate) c).ToList();
            FeedbackRating = provider.ProviderRegistrationFeedbackRating.Select(c => (ProviderFeedbackRating) c).ToList();
            FeedbackAttributes = provider.ProviderRegistrationFeedbackAttributes.Select(c => (ProviderFeedbackAttribute) c).ToList();
            DeliveryTypes = new List<DeliveryType>();
        }

        public static implicit operator ProviderLocation(ProviderStandard provider)
        {
            return new ProviderLocation
            {
                Ukprn = provider.Provider.Ukprn,
                Name = provider.Provider.Name,
                TradingName = provider.Provider.TradingName,
                Email = provider.Email,
                Phone = provider.Phone,
                ContactUrl = provider.ContactUrl,
                AchievementRates = provider.NationalAchievementRate.Select(c => (AchievementRate) c).ToList(),
                DeliveryTypes = new List<DeliveryType>(),
                FeedbackRating = provider.Provider.ProviderRegistrationFeedbackRating.Select(c => (ProviderFeedbackRating) c).ToList(),
                FeedbackAttributes = provider.Provider.ProviderRegistrationFeedbackAttributes.Select(c => (ProviderFeedbackAttribute) c).ToList()
            };
        }

        public int Ukprn { get; private set; }
        public string Name { get; private set; }
        public string TradingName { get; set; }
        public string ContactUrl { get ; private set ; }
        public string Email { get ; private set ; }
        public string Phone { get ; private set ; }
        
        public ProviderHeadOfficeAddress Address { get; set; }
        public List<DeliveryType> DeliveryTypes { get; set; }
        public List<AchievementRate> AchievementRates { get; set; }
        public List<ProviderFeedbackRating> FeedbackRating { get; set; }
        public List<ProviderFeedbackAttribute> FeedbackAttributes { get; set; }
    }
}