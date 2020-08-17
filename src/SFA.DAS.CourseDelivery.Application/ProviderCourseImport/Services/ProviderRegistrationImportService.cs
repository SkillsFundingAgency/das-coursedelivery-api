using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.ProviderCourseImport.Services
{
    public class ProviderRegistrationImportService : IProviderRegistrationImportService
    {
        private readonly ILogger<ProviderRegistrationImportService> _logger;
        private readonly IRoatpApiService _roatpApiService;
        private readonly IProviderRegistrationImportRepository _importRepository;
        private readonly IProviderRegistrationRepository _repository;
        private readonly IImportAuditRepository _auditRepository;

        public ProviderRegistrationImportService(
            ILogger<ProviderRegistrationImportService> logger,
            IRoatpApiService roatpApiService,
            IProviderRegistrationImportRepository importRepository,
            IProviderRegistrationRepository repository,
            IImportAuditRepository auditRepository)
        {
            _logger = logger;
            _roatpApiService = roatpApiService;
            _importRepository = importRepository;
            _repository = repository;
            _auditRepository = auditRepository;
        }

        public async Task ImportData()
        {
            var importStartTime = DateTime.UtcNow;

            var providerRegistrationsFromRoatp = (await _roatpApiService.GetProviderRegistrations()).ToList();

            if (providerRegistrationsFromRoatp.Count == 0)
            {
                _logger.LogInformation("No data received from ROATP. Ending import.");
                return;
            }

            var providerRegistrationImports = providerRegistrationsFromRoatp
                .Select(registration => (ProviderRegistrationImport) registration)
                .ToList();

            _logger.LogInformation("Clearing import table");
            _importRepository.DeleteAll();
            _logger.LogInformation("Populate import table");
            await _importRepository.InsertMany(providerRegistrationImports);

            _logger.LogInformation("Clearing data table");
            _repository.DeleteAll();
            _logger.LogInformation("Populate data table");
            await _repository.InsertFromImportTable();

            await _auditRepository.Insert(new ImportAudit(
                importStartTime,
                providerRegistrationImports.Count, 
                ImportType.ProviderRegistrations));
        }
    }
}