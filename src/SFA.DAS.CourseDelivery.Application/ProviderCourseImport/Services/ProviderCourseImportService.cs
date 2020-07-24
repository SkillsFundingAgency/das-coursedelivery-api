using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.ProviderCourseImport.Services
{
    public class ProviderCourseImportService
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
            
            _providerImportRepository.DeleteAll();
            _providerStandardImportRepository.DeleteAll();
            _providerStandardLocationImportRepository.DeleteAll();
            _standardLocationImportRepository.DeleteAll();

            var providers = providerCourseInformation.Select(c => (ProviderImport) c).ToList();
            
            var standardLocationImports = new List<StandardLocationImport>();
            foreach (var location in providerCourseInformation.Select(c => c.Locations))
            {
                foreach (var courseLocation in location
                    .Where(courseLocation => !standardLocationImports
                        .Exists(c => c.LocationId.Equals(courseLocation.Id))))
                {
                    standardLocationImports.Add(courseLocation);
                }
            }
            
            var providerStandardLocationImport = new List<ProviderStandardLocationImport>();
            var providerStandardImport = new List<ProviderStandardImport>();
            foreach (var provider in providerCourseInformation)
            {
                foreach (var courseStandard in provider.Standards)
                {
                    providerStandardImport.Add(new ProviderStandardImport().Map(courseStandard,provider.Ukprn));
                    providerStandardLocationImport.AddRange(courseStandard.Locations.Select(standardLocation => 
                        new ProviderStandardLocationImport().Map(standardLocation, provider.Ukprn, courseStandard.StandardCode)));
                }
            }
            
            var providerImportTask = _providerImportRepository.InsertMany(providers);
            var standardLocationImportTask = _standardLocationImportRepository.InsertMany(standardLocationImports);
            var providerStandardImportTask = _providerStandardImportRepository.InsertMany(providerStandardImport);
            var providerStandardLocationImportTask = _providerStandardLocationImportRepository.InsertMany(providerStandardLocationImport);

            await Task.WhenAll(providerImportTask, standardLocationImportTask, providerStandardImportTask, providerStandardLocationImportTask);
        }
    }
}