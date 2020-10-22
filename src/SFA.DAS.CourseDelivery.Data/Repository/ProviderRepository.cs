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

        public async Task<IEnumerable<Provider>> GetByStandardId(int standardId)
        {
            
            var providers = await _readonlyDataContext
                .ProviderStandards
                .Include(c => c.Provider)
                .ThenInclude(c=>c.ProviderRegistrationFeedbackAttributes)
                .Include(c=>c.Provider)
                .ThenInclude(c=>c.NationalAchievementRates)
                .Include(c=>c.Provider)
                .ThenInclude(c=>c.ProviderRegistrationFeedbackRating)
                .Where(c => c.StandardId.Equals(standardId))
                .Select(c=>c.Provider)
                .FilterRegisteredProviders()
                .OrderBy(c=>c.Name).ToListAsync();
            
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
                .FilterRegisteredProviders().ToListAsync();

            return providers;
        }

        public async Task UpdateAddressesFromImportTable()
        {
            var providerImports = await _dataContext
                .ProviderImports
                .Where(c=>!string.IsNullOrEmpty(c.Postcode))
                .ToListAsync();

            foreach (var providerImport in providerImports)
            {
                var provider = await _dataContext.Providers.FindAsync(providerImport.Id);
                provider.Postcode = providerImport.Postcode;
                provider.Lat = providerImport.Lat;
                provider.Long = providerImport.Long;
            }
            _dataContext.SaveChanges();
        }

        public async Task<IEnumerable<ProviderWithStandardAndLocation>> GetByStandardIdAndLocation(int standardId,
            double lat, double lon, short sortOrder)
        {
            var providers = await _readonlyDataContext.ProviderWithStandardAndLocations
                .FromSqlInterpolated(GetProviderQuery(standardId, lat, lon))
                .OrderBy(OrderProviderStandards(sortOrder))
                .ToListAsync();
            
            return providers;
        }

        public async Task<IEnumerable<ProviderWithStandardAndLocation>> GetProviderByStandardIdAndLocation(int ukprn, int standardId,
            double lat = 0, double lon = 0)
        {
            
            var provider = await _readonlyDataContext.ProviderWithStandardAndLocations.FromSqlInterpolated(GetProviderQuery(standardId, lat,lon))
                .Where(c=>c.Ukprn.Equals(ukprn))
                .OrderBy(c=>c.DistanceInMiles)
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

        private FormattableString GetProviderQuery(int standardId, double lat, double lon)
        {
            return $@"
select
    P.Ukprn,
    P.Name,
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
    PRFR.FeedbackName
from Provider P
inner join ProviderStandard PS on P.UkPrn = PS.UkPrn
inner join ProviderStandardLocation PSL on PSL.UkPrn = P.UkPrn and PSL.StandardId = PS.StandardId
inner join (select
		LocationId
		,[Name]
		,geography::Point(isnull(l.Lat,0), isnull(l.Long,0), 4326)
            .STDistance(geography::Point({lat}, {lon}, 4326)) * 0.0006213712 as DistanceInMiles
	from [StandardLocation] l) l on l.LocationId = psl.LocationId
inner join StandardLocation SL on sl.LocationId = psl.LocationId
inner join ProviderRegistration PR on PR.UkPrn = p.UkPrn
left join NationalAchievementRate NAR on NAR.UkPrn = psl.UkPrn
left join ProviderRegistrationFeedbackAttribute PRFA on PRFA.UkPrn = p.UkPrn
left join ProviderRegistrationFeedbackRating PRFR on PRFR.UkPrn = p.UkPrn
where psl.StandardId = {standardId}
and PR.StatusId = 1 AND PR.ProviderTypeId = 1
and l.DistanceInMiles <= psl.Radius";
        }

        private FormattableString GetProvidersAtLocationForStandard(int standardId, double lat, double lon)
        {
            return $@"
select distinct 
    P.*
from Provider P
inner join ProviderStandard PS on P.UkPrn = PS.UkPrn
inner join ProviderStandardLocation PSL on PSL.UkPrn = P.UkPrn and PSL.StandardId = PS.StandardId
inner join (select
		LocationId
		,[Name]
		,geography::Point(isnull(l.Lat,0), isnull(l.Long,0), 4326)
            .STDistance(geography::Point({lat}, {lon}, 4326)) * 0.0006213712 as DistanceInMiles
	from [StandardLocation] l) l on l.LocationId = psl.LocationId
inner join StandardLocation SL on sl.LocationId = psl.LocationId
inner join ProviderRegistration PR on PR.UkPrn = p.UkPrn
where psl.StandardId = {standardId}
and PR.StatusId = 1 AND PR.ProviderTypeId = 1
and l.DistanceInMiles <= psl.Radius
";
        }
    }
}