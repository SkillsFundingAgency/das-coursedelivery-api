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

        public ImportDataCommandHandler (IProviderCourseImportService providerCourseImportService, 
            INationalAchievementRatesImportService nationalAchievementRatesImportService,
            INationalAchievementRatesOverallImportService nationalAchievementRatesOverallImportService
            )
        {
            _providerCourseImportService = providerCourseImportService;
            _nationalAchievementRatesImportService = nationalAchievementRatesImportService;
            _nationalAchievementRatesOverallImportService = nationalAchievementRatesOverallImportService;
        }
        public async Task<Unit> Handle(ImportDataCommand request, CancellationToken cancellationToken)
        {
            var providerImportTask =  _providerCourseImportService.ImportProviderCourses();
            var achievementRatesImportTask = _nationalAchievementRatesImportService.ImportData();
            var achievementRatesAllImportTask = _nationalAchievementRatesOverallImportService.ImportData();

            await Task.WhenAll(providerImportTask, achievementRatesImportTask, achievementRatesAllImportTask);
            
            return Unit.Value;
        }
    }
}