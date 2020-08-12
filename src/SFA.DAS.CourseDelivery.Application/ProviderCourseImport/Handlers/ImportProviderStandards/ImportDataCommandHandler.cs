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

        public ImportDataCommandHandler (IProviderCourseImportService providerCourseImportService, INationalAchievementRatesImportService nationalAchievementRatesImportService)
        {
            _providerCourseImportService = providerCourseImportService;
            _nationalAchievementRatesImportService = nationalAchievementRatesImportService;
        }
        public async Task<Unit> Handle(ImportDataCommand request, CancellationToken cancellationToken)
        {
            var providerImportTask =  _providerCourseImportService.ImportProviderCourses();
            var achievementRatesImportTask = _nationalAchievementRatesImportService.ImportData();

            await Task.WhenAll(providerImportTask, achievementRatesImportTask);
            
            return Unit.Value;
        }
    }
}