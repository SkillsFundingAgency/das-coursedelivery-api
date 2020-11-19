using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.ProviderCourseImport.Handlers.ImportProviderStandards
{
    public class ImportDataCommandHandler : IRequestHandler<ImportDataCommand, Unit>
    {
        private readonly IProviderCourseImportService _providerCourseImportService;
        private readonly INationalAchievementRatesImportService _nationalAchievementRatesImportService;
        private readonly INationalAchievementRatesOverallImportService _nationalAchievementRatesOverallImportService;
        private readonly IProviderRegistrationImportService _providerRegistrationImportService;
        private readonly IProviderRegistrationAddressImportService _providerRegistrationAddressImportService;

        public ImportDataCommandHandler (IProviderCourseImportService providerCourseImportService, 
            INationalAchievementRatesImportService nationalAchievementRatesImportService,
            INationalAchievementRatesOverallImportService nationalAchievementRatesOverallImportService,
            IProviderRegistrationImportService providerRegistrationImportService,
            IProviderRegistrationAddressImportService providerRegistrationAddressImportService)
        {
            _providerCourseImportService = providerCourseImportService;
            _nationalAchievementRatesImportService = nationalAchievementRatesImportService;
            _nationalAchievementRatesOverallImportService = nationalAchievementRatesOverallImportService;
            _providerRegistrationImportService = providerRegistrationImportService;
            _providerRegistrationAddressImportService = providerRegistrationAddressImportService;
        }
        public async Task<Unit> Handle(ImportDataCommand request, CancellationToken cancellationToken)
        {
            await _providerCourseImportService.ImportProviderCourses();
            await _providerRegistrationImportService.ImportData();
            
            var achievementRatesImportTask = _nationalAchievementRatesImportService.ImportData();
            var achievementRatesAllImportTask = _nationalAchievementRatesOverallImportService.ImportData();
            
            await Task.WhenAll(achievementRatesImportTask, achievementRatesAllImportTask);

            await _providerRegistrationAddressImportService.ImportAddressData();
            
            return Unit.Value;
        }
    }
}