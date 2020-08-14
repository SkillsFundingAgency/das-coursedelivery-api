using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.OverallNationalAchievementRates.Services
{
    public class OverallNationalAchievementRateService : IOverallNationalAchievementRateService
    {
        private readonly INationalAchievementRateOverallRepository _repository;

        public OverallNationalAchievementRateService (INationalAchievementRateOverallRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<NationalAchievementRateOverall>> GetItemsBySectorSubjectArea(string sectorSubjectArea)
        {
            var items = await _repository.GetBySectorSubjectArea(sectorSubjectArea);
            
            return items;
        }
    }
}