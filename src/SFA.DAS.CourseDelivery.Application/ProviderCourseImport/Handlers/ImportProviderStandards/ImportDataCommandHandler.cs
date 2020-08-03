using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.ProviderCourseImport.Handlers.ImportProviderStandards
{
    public class ImportDataCommandHandler : IRequestHandler<ImportDataCommand, Unit>
    {
        private readonly IProviderCourseImportService _providerCourseImportService;

        public ImportDataCommandHandler (IProviderCourseImportService providerCourseImportService)
        {
            _providerCourseImportService = providerCourseImportService;
        }
        public async Task<Unit> Handle(ImportDataCommand request, CancellationToken cancellationToken)
        {
            await _providerCourseImportService.ImportProviderCourses();
            
            return Unit.Value;
        }
    }
}