using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
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

        public ProviderCourseImportService (
            ICourseDirectoryService courseDirectoryService,
            IProviderImportRepository providerImportRepository,
            IProviderStandardImportRepository providerStandardImportRepository,
            IProviderStandardLocationImportRepository providerStandardLocationImportRepository,
            IStandardLocationImportRepository standardLocationImportRepository)
        {
            _courseDirectoryService = courseDirectoryService;
            _providerImportRepository = providerImportRepository;
            _providerStandardImportRepository = providerStandardImportRepository;
            _providerStandardLocationImportRepository = providerStandardLocationImportRepository;
            _standardLocationImportRepository = standardLocationImportRepository;
        }
        public async Task ImportProviderCourses()
        {
            var providerCourseInformation = (await _courseDirectoryService.GetProviderCourseInformation()).ToList();

            if (!providerCourseInformation.Any())
            {
                return;
            }
            
            ClearImportTables();

            var providers = GetProviderImports(providerCourseInformation).ToList();
            var standardLocationImports = GetStandardLocationImports(providerCourseInformation).ToList();
            var providerStandardLocationImport = GetProviderStandardLocationImport(providerCourseInformation).ToList();
            var providerStandardImport = GetProviderStandardImport(providerCourseInformation).ToList();
            
            var providerImportTask = _providerImportRepository.InsertMany(providers);
            var standardLocationImportTask = _standardLocationImportRepository.InsertMany(standardLocationImports);
            var providerStandardImportTask = _providerStandardImportRepository.InsertMany(providerStandardImport);
            var providerStandardLocationImportTask = _providerStandardLocationImportRepository.InsertMany(providerStandardLocationImport);

            await Task.WhenAll(providerImportTask, standardLocationImportTask, providerStandardImportTask, providerStandardLocationImportTask);
        }

        private static IEnumerable<ProviderImport> GetProviderImports(IEnumerable<Provider> providerCourseInformation)
        {
            return providerCourseInformation.Select(c => (ProviderImport) c);
        }

        private static IEnumerable<StandardLocationImport> GetStandardLocationImports(IEnumerable<Provider> providerCourseInformation)
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

        private static IEnumerable<ProviderStandardImport> GetProviderStandardImport(IEnumerable<Provider> providerCourseInformation)
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

        private static IEnumerable<ProviderStandardLocationImport> GetProviderStandardLocationImport(IEnumerable<Provider> providerCourseInformation)
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

        private void ClearImportTables()
        {
            _providerImportRepository.DeleteAll();
            _providerStandardImportRepository.DeleteAll();
            _providerStandardLocationImportRepository.DeleteAll();
            _standardLocationImportRepository.DeleteAll();
        }
    }
}