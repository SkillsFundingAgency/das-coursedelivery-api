using System;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.ProviderCourseImport.Services
{
    public class ProviderRegistrationAddressImportService : IProviderRegistrationAddressImportService
    {
        private readonly IRoatpApiService _roatpApiService;
        private readonly IPostcodeApiService _postcodeApiService;
        private readonly IProviderRepository _providerRepository;
        private readonly IProviderImportRepository _providerImportRepository;
        private readonly IImportAuditRepository _importAuditRepository;

        public ProviderRegistrationAddressImportService (
            IRoatpApiService roatpApiService, 
            IPostcodeApiService postcodeApiService, 
            IProviderRepository providerRepository, 
            IProviderImportRepository providerImportRepository,
            IImportAuditRepository importAuditRepository)
        {
            _roatpApiService = roatpApiService;
            _postcodeApiService = postcodeApiService;
            _providerRepository = providerRepository;
            _providerImportRepository = providerImportRepository;
            _importAuditRepository = importAuditRepository;
        }

        public async Task ImportAddressData()
        {
            var importStartTime = DateTime.UtcNow;
            
            var providers = (await _providerRepository.GetAllRegistered()).Select(c=>c.Ukprn).ToList();

            var skip = 0;
            var providersToProcess = providers.Skip(skip).Take(100).ToList();

            while (providersToProcess.Any())
            {
                var registrationData = await _roatpApiService.GetProviderRegistrationLookupData(providersToProcess);

                var postCodeData = (await _postcodeApiService.GetPostcodeData(new PostcodeLookupRequest
                {
                    Postcodes = registrationData.Results.SelectMany(c => c.ContactDetails)
                        .Where(c => c.ContactType == "P").Select(x => x.ContactAddress.PostCode).ToList()
                })).Result.ToList();

                foreach (var provider in registrationData.Results.Where(c=>c.ContactDetails!=null))
                {
                    var providerPostcode = provider.ContactDetails.Where(x => x.ContactType.Equals("P"))
                        .Select(c => c.ContactAddress.PostCode).FirstOrDefault();

                    if (!string.IsNullOrEmpty(providerPostcode))
                    {
                        var providerAddressData = postCodeData
                            .FirstOrDefault(x => x.Query == providerPostcode);

                        if (providerAddressData?.Result != null)
                        {
                            await _providerImportRepository.UpdateAddress(provider.Ukprn,
                                providerAddressData.Result.Postcode,
                                providerAddressData.Result.Latitude, providerAddressData.Result.Longitude);
                        }
                    }
                }
                
                
                skip += 100;
                providersToProcess = providers.Skip(skip).Take(100).ToList();
            }

            await _providerRepository.UpdateAddressesFromImportTable();
            
            await _importAuditRepository.Insert(new ImportAudit(
                importStartTime,
                providers.Count, 
                ImportType.ProviderAddressData));
        }
    }
}