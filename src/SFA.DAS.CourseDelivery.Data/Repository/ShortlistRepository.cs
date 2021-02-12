using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class ShortlistRepository : IShortlistRepository
    {
        private readonly ILogger<ShortlistRepository> _logger;
        private readonly ICourseDeliveryDataContext _dataContext;
        private readonly ICourseDeliveryReadonlyDataContext _readonlyDataContext;

        public ShortlistRepository(
            ILogger<ShortlistRepository> logger,
            ICourseDeliveryDataContext dataContext, 
            ICourseDeliveryReadonlyDataContext readonlyDataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
            _readonlyDataContext = readonlyDataContext;
        }

        public async Task<IEnumerable<Shortlist>> GetAllForUser(Guid userId)
        {
            return await _readonlyDataContext.Shortlists
                .Where(shortlist => shortlist.ShortlistUserId == userId)
                .ToListAsync();
        }

        public async Task<Shortlist> GetShortlistUserItem(Shortlist item)
        {
            return await _readonlyDataContext.Shortlists.SingleOrDefaultAsync(c=>
                c.ShortlistUserId.Equals(item.ShortlistUserId)
                && c.StandardId.Equals(item.StandardId)
                && c.Ukprn.Equals(item.Ukprn)
                && c.Lat.Equals(item.Lat)
                && c.Long.Equals(item.Long)
                );
        }

        public async Task Insert(Shortlist item)
        {
            try
            {
                await _dataContext.Shortlists.AddAsync(item);
                _dataContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                _logger.LogInformation(e, "Unable to add shortlist item");
            }
        }

        public async Task<IEnumerable<ShortlistProviderWithStandardAndLocation>> GetShortListForUser(Guid userId)
        {
            var shortlistItems = await _readonlyDataContext.ShortlistProviderWithStandardAndLocations
                .FromSqlInterpolated(GetProvidersShortlistQuery(userId)).ToListAsync();

            return shortlistItems;
        }

        public void Delete(Guid id, Guid shortlistUserId)
        {
            var shortlistItem =
                _dataContext.Shortlists.SingleOrDefault(c => c.Id.Equals(id) && c.ShortlistUserId.Equals(shortlistUserId));
            if (shortlistItem != null)
            {
                _dataContext.Shortlists.Remove(shortlistItem);
                _dataContext.SaveChanges();
            }
        }

        private FormattableString GetProvidersShortlistQuery(Guid userId)
        {
            return $@"
select
    P.Ukprn,
    P.Name,
    p.TradingName,
    ps.ContactUrl,
    ps.Email,
    ps.Phone,
    sl.LocationId,
    sl.Address1,
    sl.Address2,
    sl.Town,
    sl.Postcode,
    sl.County,
    psl.DeliveryModes,
    psl.[National],
    case when shl.Long is null then CAST(0.0 as float)
       else l.DistanceInMiles END as DistanceInMiles,
    NAR.Id,
    NAR.Age,
    NAR.SectorSubjectArea,
    NAR.ApprenticeshipLevel,
    NAR.OverallCohort,
    NAR.OverallAchievementRate,
    '' as AttributeName,
    0 as Strength,
    0 as Weakness,
    PRFR.FeedbackCount,
    PRFR.FeedbackName,
    CAST(0.0 as float) as ProviderDistanceInMiles,
    pr.Address1 ProviderHeadOfficeAddress1,
    pr.Address2 ProviderHeadOfficeAddress2,
    pr.Address3 ProviderHeadOfficeAddress3,
    pr.Address4 ProviderHeadOfficeAddress4,
    pr.Town ProviderHeadOfficeTown,
    pr.PostCode ProviderHeadOfficePostcode,
    Shl.StandardId as StandardId,
    Shl.LocationDescription,
    Shl.ShortlistUserId,
    Shl.Id as ShortlistId,
    Shl.CreatedDate as CreatedDate
from Provider P
    inner join ProviderStandard PS on PS.UkPrn = P.UkPrn
    inner join ProviderStandardLocation PSL on PSL.UkPrn = P.UkPrn and PSL.StandardId = PS.StandardId
    inner join StandardLocation SL on sl.LocationId = psl.LocationId
    inner join ProviderRegistration PR on PR.UkPrn = p.UkPrn
    inner join Shortlist Shl on Shl.StandardId = ps.StandardId and Shl.UkPrn = p.UkPrn
    left join NationalAchievementRate NAR on NAR.UkPrn = p.UkPrn and NAR.SectorSubjectArea = Shl.CourseSector and NAR.Age=4
    left join ProviderRegistrationFeedbackRating PRFR on PRFR.UkPrn = p.UkPrn
    left join (select
                     l.LocationId
                      ,[Name]
                      ,geography::Point(isnull(l.Lat,0), isnull(l.Long,0), 4326)
                           .STDistance(geography::Point(isnull(slx.Lat,0), isnull(slx.Long,0), 4326)) * 0.0006213712 as DistanceInMiles
                 from [StandardLocation] l
                inner join ProviderStandardLocation pslx on pslx.LocationId = l.LocationId
        inner join Shortlist slx on slx.StandardId = pslx.StandardId and slx.UkPrn = pslx.UkPrn
        ) l on l.LocationId = psl.LocationId
where
  PR.StatusId = 1 AND PR.ProviderTypeId = 1 and Shl.ShortlistUserId = {userId}";
        }
    }
}