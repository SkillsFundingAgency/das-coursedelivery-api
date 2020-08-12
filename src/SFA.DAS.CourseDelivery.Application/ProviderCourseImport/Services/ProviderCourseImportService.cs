using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.ProviderCourseImport.Services
{
    public class ProviderCourseImportService : IProviderCourseImportService
    {
        private readonly ICourseDirectoryService _courseDirectoryService;
        private readonly IProviderImportRepository _providerImportRepository;
        private readonly IProviderStandardImportRepository _providerStandardImportRepository;
        private readonly IProviderStandardLocationImportRepository _providerStandardLocationImportRepository;
        private readonly IStandardLocationImportRepository _standardLocationImportRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IProviderStandardRepository _providerStandardRepository;
        private readonly IProviderStandardLocationRepository _providerStandardLocationRepository;
        private readonly IStandardLocationRepository _standardLocationRepository;
        private readonly ILogger<ProviderCourseImportService> _logger;
        private readonly IImportAuditRepository _importAuditRepository;

        public ProviderCourseImportService (
            ICourseDirectoryService courseDirectoryService,
            IProviderImportRepository providerImportRepository,
            IProviderStandardImportRepository providerStandardImportRepository,
            IProviderStandardLocationImportRepository providerStandardLocationImportRepository,
            IStandardLocationImportRepository standardLocationImportRepository,
            IProviderRepository providerRepository,
            IProviderStandardRepository providerStandardRepository,
            IProviderStandardLocationRepository providerStandardLocationRepository,
            IStandardLocationRepository standardLocationRepository,
            IImportAuditRepository importAuditRepository,
            ILogger<ProviderCourseImportService> logger)
        {
            _courseDirectoryService = courseDirectoryService;
            _providerImportRepository = providerImportRepository;
            _providerStandardImportRepository = providerStandardImportRepository;
            _providerStandardLocationImportRepository = providerStandardLocationImportRepository;
            _standardLocationImportRepository = standardLocationImportRepository;
            _providerRepository = providerRepository;
            _providerStandardRepository = providerStandardRepository;
            _providerStandardLocationRepository = providerStandardLocationRepository;
            _standardLocationRepository = standardLocationRepository;
            _importAuditRepository = importAuditRepository;
            _logger = logger;
        }
        public async Task ImportProviderCourses()
        {
            var importStartTime = DateTime.UtcNow;
            
            _logger.LogInformation("Getting data from Course Directory");
            var providerCourseInformation = (await _courseDirectoryService.GetProviderCourseInformation()).ToList();

            if (!providerCourseInformation.Any())
            {
                _logger.LogInformation("No data received from Course Directory. Ending import.");
                return;
            }
            
            _logger.LogInformation("Clearing import tables");
            ClearImportTables();

            var providers = GetProviderImports(providerCourseInformation).ToList();
            var standardLocationImports = GetStandardLocationImports(providerCourseInformation).ToList();
            var providerStandardLocationImport = GetProviderStandardLocationImport(providerCourseInformation).ToList();
            var providerStandardImport = GetProviderStandardImport(providerCourseInformation).ToList();
            
            var providerImportTask = _providerImportRepository.InsertMany(providers);
            var standardLocationImportTask = _standardLocationImportRepository.InsertMany(standardLocationImports);
            var providerStandardImportTask = _providerStandardImportRepository.InsertMany(providerStandardImport);
            var providerStandardLocationImportTask = _providerStandardLocationImportRepository.InsertMany(providerStandardLocationImport);

            _logger.LogInformation($"Adding {providers.Count} providers to import tables");
            await Task.WhenAll(providerImportTask, standardLocationImportTask, providerStandardImportTask, providerStandardLocationImportTask);
            _logger.LogInformation("Finishing adding to import table - clearing main data table");
            
            ClearDataTables();

            _logger.LogInformation("Adding to tables from import tables");
            await LoadDataFromImportTables();

            await _importAuditRepository.Insert(new ImportAudit(importStartTime, providers.Count));
            
            _logger.LogInformation("Finished import");
        }

        private static IEnumerable<ProviderImport> GetProviderImports(IEnumerable<Domain.ImportTypes.Provider> providerCourseInformation)
        {
            return providerCourseInformation.Select(c => (ProviderImport) c);
        }

        private static IEnumerable<StandardLocationImport> GetStandardLocationImports(IEnumerable<Domain.ImportTypes.Provider> providerCourseInformation)
        {
            var standardLocationImports = new List<StandardLocationImport>();
            
            foreach (var location in providerCourseInformation.Select(c => c.Locations))
            {
                standardLocationImports.AddRange(location.Select(courseLocation => (StandardLocationImport) courseLocation));
            }

            return standardLocationImports
                .GroupBy(c => c.LocationId)
                .Select(item => item.First());
        }

        private static IEnumerable<ProviderStandardImport> GetProviderStandardImport(IEnumerable<Domain.ImportTypes.Provider> providerCourseInformation)
        {
            var providerStandardImport = new List<ProviderStandardImport>();
            foreach (var provider in providerCourseInformation)
            {
                providerStandardImport.AddRange(provider.Standards.Select(courseStandard => new ProviderStandardImport().Map(courseStandard, provider.Ukprn)));
            }
            return providerStandardImport
                .GroupBy(c => new {c.Ukprn, c.StandardId})
                .Select(item => item.First());
        }

        private static IEnumerable<ProviderStandardLocationImport> GetProviderStandardLocationImport(IEnumerable<Domain.ImportTypes.Provider> providerCourseInformation)
        {
            var providerStandardLocationImport = new List<ProviderStandardLocationImport>();
            foreach (var provider in providerCourseInformation)
            {
                foreach (var courseStandard in provider.Standards)
                {
                
                    providerStandardLocationImport.AddRange(courseStandard
                        .Locations
                        .Select(standardLocation => 
                            new ProviderStandardLocationImport().Map(standardLocation, provider.Ukprn, courseStandard.StandardCode)));
                }
            }

            return providerStandardLocationImport
                .GroupBy(c => new {c.Ukprn, c.LocationId, c.StandardId})
                .Select(item => item.First());
        }

        private async Task LoadDataFromImportTables()
        {
            var insertProviderTask = 
                _providerRepository.InsertFromImportTable();
            var insertProviderStandardTask =
                _providerStandardRepository.InsertFromImportTable();
            var insertProviderStandardLocationTask =
                _providerStandardLocationRepository.InsertFromImportTable();
            var insertStandardLocationTask =
                _standardLocationRepository.InsertFromImportTable();


            await Task.WhenAll(
                insertProviderTask, 
                insertProviderStandardTask, 
                insertProviderStandardLocationTask,
                insertStandardLocationTask);
        }

        private void ClearImportTables()
        {
            _providerImportRepository.DeleteAll();
            _providerStandardImportRepository.DeleteAll();
            _providerStandardLocationImportRepository.DeleteAll();
            _standardLocationImportRepository.DeleteAll();
        }

        private void ClearDataTables()
        {
            _providerRepository.DeleteAll();
            _providerStandardRepository.DeleteAll();
            _providerStandardLocationRepository.DeleteAll();
            _standardLocationRepository.DeleteAll();
        }
    }
}