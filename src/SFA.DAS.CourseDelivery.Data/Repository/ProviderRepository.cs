using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CourseDelivery.Data.Extensions;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;
        private readonly ICourseDeliveryReadonlyDataContext _readonlyDataContext;

        public ProviderRepository(ICourseDeliveryDataContext dataContext, ICourseDeliveryReadonlyDataContext readonlyDataContext)
        {
            _dataContext = dataContext;
            _readonlyDataContext = readonlyDataContext;
        }

        public async Task InsertMany(IEnumerable<Provider> providers)
        {
            await _dataContext.Providers.AddRangeAsync(providers);
            _dataContext.SaveChanges();
        }

        public void DeleteAll()
        {
            _dataContext.Providers.RemoveRange(_dataContext.Providers.ToList());
            _dataContext.SaveChanges();
        }

        public async Task<IEnumerable<ProviderWithStandardAndLocation>> GetByStandardId( int standardId,
            string sectorSubjectArea, short level, Guid shortlistUserId)
        {
            
            var providers = await _readonlyDataContext.ProviderWithStandardAndLocations
                .FromSqlInterpolated(GetProvidersListNoLocationQuery(standardId, sectorSubjectArea, level, shortlistUserId))
                .OrderBy(p=>p.Name)
                .ThenByDescending(p=>p.National)
                .ToListAsync();
            
            return providers;
        }
        
        public async Task<IEnumerable<ProviderWithStandardAndLocation>> GetByUkprnAndStandardId(int ukprn, int standardId, string sectorSubjectArea)
        {
            var providers = await _readonlyDataContext.ProviderWithStandardAndLocations
                .FromSqlInterpolated(GetProviderNoLocationQuery(standardId, sectorSubjectArea,ukprn))
                .OrderBy(p=>p.Name)
                .ThenByDescending(p=>p.National)
            
                .ToListAsync();
            
            return providers;
        }

        public async Task<Provider> GetByUkprn(int ukPrn)
        {
            var provider = await _readonlyDataContext.Providers
                .FilterRegisteredProviders()
                .SingleOrDefaultAsync(c => c.Ukprn.Equals(ukPrn));

            return provider;
        }

        public async Task<List<Provider>> GetAllRegistered()
        {
            var providers = await _readonlyDataContext.Providers
                .Include(provider => provider.ProviderRegistration)
                .FilterRegisteredProviders().ToListAsync();

            return providers;
        }

        public async Task<IEnumerable<ProviderWithStandardAndLocation>> GetByStandardIdAndLocation(    int standardId,
            double lat, double lon, short sortOrder, string sectorSubjectArea, short level, Guid shortlistUserId)
        {
            var providers = await _readonlyDataContext.ProviderWithStandardAndLocations
                .FromSqlInterpolated(GetProviderListQuery(standardId, lat, lon, sectorSubjectArea, level, shortlistUserId))
                .OrderBy(OrderProviderStandards(sortOrder))
                .ThenByDescending(c=>c.National)
                .ToListAsync();
            return providers;
        }

        public async Task<IEnumerable<ProviderWithStandardAndLocation>> GetProviderByStandardIdAndLocation(int ukprn, int standardId,
            double lat = 0, double lon = 0, string sectorSubjectArea = "")
        {
            
            var provider = await _readonlyDataContext.ProviderWithStandardAndLocations.FromSqlInterpolated(GetProviderQuery(standardId, lat,lon, sectorSubjectArea, ukprn))
                .OrderBy(c=>c.DistanceInMiles)
                .ThenByDescending(c=>c.National)
                .ToListAsync();
            return provider;
        }

        public async Task<IEnumerable<int>> GetUkprnsForStandardAndLocation(int standardId, double lat, double lon)
        {

            var providers = await _readonlyDataContext.Providers
                .FromSqlInterpolated(GetProvidersAtLocationForStandard(standardId, lat, lon))
                .Select(c => c.Ukprn)
                .ToListAsync();

            return providers;
        }

        private static Expression<Func<ProviderWithStandardAndLocation, object>> OrderProviderStandards(short sortOrder)
        {
            if (sortOrder == 0)
            {
                return c => c.DistanceInMiles;
            }
            return c=>c.Name;
        }
        
        private FormattableString GetProvidersListNoLocationQuery(int standardId, string sectorSubjectArea, short level, Guid shortlistUserId)
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
    CAST(0.0 as float) as DistanceInMiles,
    NAR.Id,
    NAR.Age, 
    NAR.SectorSubjectArea, 
    NAR.ApprenticeshipLevel,
    NAR.OverallCohort, 
    NAR.OverallAchievementRate,
    null as AttributeName, 
    null as Strength, 
    null as Weakness,
    PRFR.FeedbackCount, 
    PRFR.FeedbackName,
    CAST(0.0 as float) as ProviderDistanceInMiles,
    pr.Address1 ProviderHeadOfficeAddress1,
    pr.Address2 ProviderHeadOfficeAddress2,
    pr.Address3 ProviderHeadOfficeAddress3,
    pr.Address4 ProviderHeadOfficeAddress4,
    pr.Town ProviderHeadOfficeTown,
    pr.PostCode ProviderHeadOfficePostcode,
    shl.Id as ShortlistId
from Provider P
inner join ProviderStandard PS on PS.UkPrn = P.UkPrn
inner join ProviderStandardLocation PSL on PSL.UkPrn = P.UkPrn and PSL.StandardId = PS.StandardId
inner join StandardLocation SL on sl.LocationId = psl.LocationId
inner join ProviderRegistration PR on PR.UkPrn = p.UkPrn
left join NationalAchievementRate NAR on NAR.UkPrn = p.UkPrn and NAR.SectorSubjectArea = {sectorSubjectArea} 
    and NAR.Age = 4 and NAR.ApprenticeshipLevel in ({level},1) 
left join ProviderRegistrationFeedbackRating PRFR on PRFR.UkPrn = p.UkPrn
left join Shortlist shl on shl.UkPrn = p.UkPrn and shl.Lat is null and shl.long is null 
    and shl.StandardId = PS.StandardId and shl.ShortlistUserId = {shortlistUserId} 
where ps.StandardId = {standardId}

and PR.StatusId = 1 AND PR.ProviderTypeId = 1";
        }

        private FormattableString GetProviderNoLocationQuery(int standardId, string sectorSubjectArea, int ukprn)
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
    CAST(0.0 as float) as DistanceInMiles,
    NAR.Id,
    NAR.Age, 
    NAR.SectorSubjectArea, 
    NAR.ApprenticeshipLevel,
    NAR.OverallCohort, 
    NAR.OverallAchievementRate,
    PRFA.AttributeName, 
    PRFA.Strength, 
    PRFA.Weakness,
    PRFR.FeedbackCount, 
    PRFR.FeedbackName,
    CAST(0.0 as float) as ProviderDistanceInMiles,
    pr.Address1 ProviderHeadOfficeAddress1,
    pr.Address2 ProviderHeadOfficeAddress2,
    pr.Address3 ProviderHeadOfficeAddress3,
    pr.Address4 ProviderHeadOfficeAddress4,
    pr.Town ProviderHeadOfficeTown,
    pr.PostCode ProviderHeadOfficePostcode,
    null as ShortlistId
from Provider P
inner join ProviderStandard PS on PS.UkPrn = P.UkPrn
inner join ProviderStandardLocation PSL on PSL.UkPrn = P.UkPrn and PSL.StandardId = PS.StandardId
inner join StandardLocation SL on sl.LocationId = psl.LocationId
inner join ProviderRegistration PR on PR.UkPrn = p.UkPrn
left join NationalAchievementRate NAR on NAR.UkPrn = p.UkPrn and NAR.SectorSubjectArea = {sectorSubjectArea} and NAR.Age = 4  
left join ProviderRegistrationFeedbackAttribute PRFA on PRFA.UkPrn = p.UkPrn
left join ProviderRegistrationFeedbackRating PRFR on PRFR.UkPrn = p.UkPrn
where ps.StandardId = {standardId}
and P.UkPrn = {ukprn}
and PR.StatusId = 1 AND PR.ProviderTypeId = 1";
        }

        private FormattableString GetProviderListQuery( int standardId, double lat, double lon,
            string sectorSubjectArea, short level, Guid shortlistUserId)
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
    l.DistanceInMiles,
    NAR.Id,
    NAR.Age, 
    NAR.SectorSubjectArea, 
    NAR.ApprenticeshipLevel,
    NAR.OverallCohort, 
    NAR.OverallAchievementRate,
    null as AttributeName, 
    null as Strength, 
    null as Weakness,
    PRFR.FeedbackCount, 
    PRFR.FeedbackName,
    pdist.DistanceInMiles as ProviderDistanceInMiles,
    pr.Address1 ProviderHeadOfficeAddress1,
    pr.Address2 ProviderHeadOfficeAddress2,
    pr.Address3 ProviderHeadOfficeAddress3,
    pr.Address4 ProviderHeadOfficeAddress4,
    pr.Town ProviderHeadOfficeTown,
    pr.PostCode ProviderHeadOfficePostcode,
    shl.Id as ShortlistId
from Provider P
inner join ProviderStandard PS on PS.UkPrn = P.UkPrn
inner join ProviderStandardLocation PSL on PSL.UkPrn = P.UkPrn and PSL.StandardId = PS.StandardId 
inner join (select
		LocationId
		,[Name]
		,geography::Point(isnull(l.Lat,0), isnull(l.Long,0), 4326)
            .STDistance(geography::Point({lat}, {lon}, 4326)) * 0.0006213712 as DistanceInMiles
	from [StandardLocation] l) l on l.LocationId = psl.LocationId
inner join (select ukprn,
                   case when isnull(Lat,0) <> 0 and isnull(Long,0) <> 0 then
                        geography::Point(isnull(Lat,0), isnull(Long,0), 4326)
                            .STDistance(geography::Point({lat}, {lon}, 4326)) * 0.0006213712 
                    else -1 end as DistanceInMiles from [ProviderRegistration]) pdist on pdist.ukprn = P.ukprn
inner join StandardLocation SL on sl.LocationId = psl.LocationId
inner join ProviderRegistration PR on PR.UkPrn = p.UkPrn
left join NationalAchievementRate NAR on NAR.UkPrn = p.UkPrn and NAR.SectorSubjectArea = {sectorSubjectArea} and NAR.Age = 4 and NAR.ApprenticeshipLevel in ({level},1) 
left join ProviderRegistrationFeedbackRating PRFR on PRFR.UkPrn = p.UkPrn
left join Shortlist shl on shl.UkPrn = p.UkPrn and Round(shl.Lat,3) = Round({lat},3) and Round(shl.long,3) = Round({lon},3) 
    and shl.StandardId = PS.StandardId and shl.ShortlistUserId = {shortlistUserId} 
where ps.StandardId = {standardId}
and PR.StatusId = 1 AND PR.ProviderTypeId = 1
and l.DistanceInMiles <= psl.Radius";
        }
        
        private FormattableString GetProviderQuery(int standardId, double lat, double lon, string sectorSubjectArea, int ukprn)
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
    l.DistanceInMiles,
    NAR.Id,
    NAR.Age, 
    NAR.SectorSubjectArea, 
    NAR.ApprenticeshipLevel,
    NAR.OverallCohort, 
    NAR.OverallAchievementRate,
    PRFA.AttributeName, 
    PRFA.Strength, 
    PRFA.Weakness,
    PRFR.FeedbackCount, 
    PRFR.FeedbackName,
    pdist.DistanceInMiles as ProviderDistanceInMiles,
    pr.Address1 ProviderHeadOfficeAddress1,
    pr.Address2 ProviderHeadOfficeAddress2,
    pr.Address3 ProviderHeadOfficeAddress3,
    pr.Address4 ProviderHeadOfficeAddress4,
    pr.Town ProviderHeadOfficeTown,
    pr.PostCode ProviderHeadOfficePostcode,
    null as ShortlistId
from Provider P
inner join ProviderStandard PS on PS.UkPrn = P.UkPrn
inner join ProviderStandardLocation PSL on PSL.UkPrn = P.UkPrn and PSL.StandardId = PS.StandardId 
inner join (select
		LocationId
		,[Name]
		,geography::Point(isnull(l.Lat,0), isnull(l.Long,0), 4326)
            .STDistance(geography::Point({lat}, {lon}, 4326)) * 0.0006213712 as DistanceInMiles
	from [StandardLocation] l) l on l.LocationId = psl.LocationId
inner join (select ukprn,
                   case when isnull(Lat,0) <> 0 and isnull(Long,0) <> 0 then
                        geography::Point(isnull(Lat,0), isnull(Long,0), 4326)
                            .STDistance(geography::Point({lat}, {lon}, 4326)) * 0.0006213712 
                    else -1 end as DistanceInMiles from [ProviderRegistration]) pdist on pdist.ukprn = P.ukprn
inner join StandardLocation SL on sl.LocationId = psl.LocationId
inner join ProviderRegistration PR on PR.UkPrn = p.UkPrn
left join NationalAchievementRate NAR on NAR.UkPrn = p.UkPrn and NAR.SectorSubjectArea = {sectorSubjectArea} and NAR.Age=4
left join ProviderRegistrationFeedbackAttribute PRFA on PRFA.UkPrn = p.UkPrn
left join ProviderRegistrationFeedbackRating PRFR on PRFR.UkPrn = p.UkPrn
where ps.StandardId = {standardId}
and P.UkPrn = {ukprn}
and PR.StatusId = 1 AND PR.ProviderTypeId = 1
and l.DistanceInMiles <= psl.Radius";
        }

        private FormattableString GetProvidersAtLocationForStandard(int standardId, double lat, double lon)
        {
            return $@"
select distinct 
    P.*
from Provider P
inner join ProviderStandard PS on PS.UkPrn = P.UkPrn
inner join ProviderStandardLocation PSL on PSL.UkPrn = P.UkPrn and PSL.StandardId = PS.StandardId
inner join (select
		LocationId
		,[Name]
		,geography::Point(isnull(l.Lat,0), isnull(l.Long,0), 4326)
            .STDistance(geography::Point({lat}, {lon}, 4326)) * 0.0006213712 as DistanceInMiles
	from [StandardLocation] l) l on l.LocationId = psl.LocationId
inner join StandardLocation SL on sl.LocationId = psl.LocationId
inner join ProviderRegistration PR on PR.UkPrn = p.UkPrn
where ps.StandardId = {standardId}
and PR.StatusId = 1 AND PR.ProviderTypeId = 1
and l.DistanceInMiles <= psl.Radius
";
        }
    }
}