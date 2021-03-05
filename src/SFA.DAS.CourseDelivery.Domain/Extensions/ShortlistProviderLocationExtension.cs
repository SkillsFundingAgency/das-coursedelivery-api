using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Domain.Extensions
{
    public static class ShortlistProviderLocationExtension
    {
        public static IEnumerable<Models.Shortlist> BuildShortlistProviderLocation(this IEnumerable<ShortlistProviderWithStandardAndLocation> shortlistProviders)
        {
            return shortlistProviders
                .GroupBy(item => new
                {
                    UkPrn = item.Ukprn, 
                    item.Name, 
                    item.TradingName,
                    item.ContactUrl, 
                    item.Email, 
                    item.Phone, 
                    item.ProviderDistanceInMiles,
                    item.ProviderHeadOfficeAddress1,
                    item.ProviderHeadOfficeAddress2,
                    item.ProviderHeadOfficeAddress3,
                    item.ProviderHeadOfficeAddress4,
                    item.ProviderHeadOfficeTown,
                    item.ProviderHeadOfficePostcode,
                    item.ShortlistId,
                    item.ShortlistUserId,
                    CourseId = item.StandardId,
                    item.LocationDescription,
                    item.CreatedDate,
                    item.StandardInfoUrl,
                    item.MarketingInfo
                })
                .Select(group => new Models.Shortlist
                {
                    Id = group.Key.ShortlistId ?? Guid.Empty,
                    StandardId = group.Key.CourseId,
                    LocationDescription = group.Key.LocationDescription,
                    ShortlistUserId = group.Key.ShortlistUserId,
                    CreatedDate = group.Key.CreatedDate,
                    ProviderLocation = new ProviderLocation(
                        group.Key.UkPrn, 
                        group.Key.Name,
                        group.Key.TradingName,
                        group.Key.ContactUrl, 
                        group.Key.Phone, 
                        group.Key.Email, 
                        group.Key.StandardInfoUrl,
                        group.Key.ProviderDistanceInMiles,
                        group.Key.ProviderHeadOfficeAddress1,
                        group.Key.ProviderHeadOfficeAddress2,
                        group.Key.ProviderHeadOfficeAddress3,
                        group.Key.ProviderHeadOfficeAddress4,
                        group.Key.ProviderHeadOfficeTown,
                        group.Key.ProviderHeadOfficePostcode,
                        group.Key.ShortlistId,
                        group.Key.MarketingInfo,
                        group.Select(c=>(ProviderWithStandardAndLocation)c).ToList())
                })
                .ToList();
        }
    }
}