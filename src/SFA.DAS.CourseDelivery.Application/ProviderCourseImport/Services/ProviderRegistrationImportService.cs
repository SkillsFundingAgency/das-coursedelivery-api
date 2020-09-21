using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using ProviderRegistration = SFA.DAS.CourseDelivery.Domain.ImportTypes.ProviderRegistration;

namespace SFA.DAS.CourseDelivery.Application.ProviderCourseImport.Services
{
    public class ProviderRegistrationImportService : IProviderRegistrationImportService
    {
        private readonly ILogger<ProviderRegistrationImportService> _logger;
        private readonly IRoatpApiService _roatpApiService;
        private readonly IProviderRegistrationImportRepository _providerRegistrationImportRepository;
        private readonly IProviderRegistrationRepository _providerRegistrationRepository;
        private readonly IProviderRegistrationFeedbackAttributeRepository _providerRegistrationFeedbackAttributeRepository;
        private readonly IProviderRegistrationFeedbackAttributeImportRepository _providerRegistrationFeedbackAttributeImportRepository;
        private readonly IProviderRegistrationFeedbackRatingRepository _providerRegistrationFeedbackRatingRepository;
        private readonly IProviderRegistrationFeedbackRatingImportRepository _providerRegistrationFeedbackRatingImportRepository;
        private readonly IImportAuditRepository _auditRepository;

        public ProviderRegistrationImportService(
            ILogger<ProviderRegistrationImportService> logger,
            IRoatpApiService roatpApiService,
            IProviderRegistrationImportRepository providerRegistrationImportRepository,
            IProviderRegistrationRepository providerRegistrationRepository,
            IProviderRegistrationFeedbackAttributeRepository providerRegistrationFeedbackAttributeRepository,
            IProviderRegistrationFeedbackAttributeImportRepository providerRegistrationFeedbackAttributeImportRepository,
            IProviderRegistrationFeedbackRatingRepository providerRegistrationFeedbackRatingRepository,
            IProviderRegistrationFeedbackRatingImportRepository providerRegistrationFeedbackRatingImportRepository,
            IImportAuditRepository auditRepository)
        {
            _logger = logger;
            _roatpApiService = roatpApiService;
            _providerRegistrationImportRepository = providerRegistrationImportRepository;
            _providerRegistrationRepository = providerRegistrationRepository;
            _providerRegistrationFeedbackAttributeRepository = providerRegistrationFeedbackAttributeRepository;
            _providerRegistrationFeedbackAttributeImportRepository = providerRegistrationFeedbackAttributeImportRepository;
            _providerRegistrationFeedbackRatingRepository = providerRegistrationFeedbackRatingRepository;
            _providerRegistrationFeedbackRatingImportRepository = providerRegistrationFeedbackRatingImportRepository;
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

            _logger.LogInformation("Clearing import table");
            ClearImportTables();
            _logger.LogInformation("Populate import table");
            
            var providerRegistrationImports = await PopulateImportTables(providerRegistrationsFromRoatp);

            _logger.LogInformation("Clearing data table");
            ClearMainTables();
            _logger.LogInformation("Populate data table");


            var feedbackRating = _providerRegistrationFeedbackRatingImportRepository.GetAll();
            var feedbackAttributes = _providerRegistrationFeedbackAttributeImportRepository.GetAll();

            await Task.WhenAll(feedbackRating, feedbackAttributes);
            
            var insertProviderTask = _providerRegistrationRepository.InsertFromImportTable();
            var insertProviderRatingTask =
                _providerRegistrationFeedbackRatingRepository.InsertMany(feedbackRating.Result
                    .Select(c => (ProviderRegistrationFeedbackRating)c).ToList());
            var insertProviderAttributeTask =
                _providerRegistrationFeedbackAttributeRepository.InsertMany(feedbackAttributes.Result
                    .Select(c => (ProviderRegistrationFeedbackAttribute) c).ToList());

            await Task.WhenAll(insertProviderTask, insertProviderAttributeTask, insertProviderRatingTask);
            
            await _auditRepository.Insert(new ImportAudit(
                importStartTime,
                providerRegistrationImports.Count, 
                ImportType.ProviderRegistrations));
        }

        private async Task<List<ProviderRegistrationImport>> PopulateImportTables(IReadOnlyCollection<ProviderRegistration> providerRegistrationsFromRoatp)
        {
            var providerRegistrationImports = providerRegistrationsFromRoatp
                .Select(registration => (ProviderRegistrationImport) registration)
                .ToList();
            var feedbackRatings = new List<ProviderRegistrationFeedbackRatingImport>();
            var feedbackAttributes = new List<ProviderRegistrationFeedbackAttributeImport>();

            foreach (var providerRegistration in providerRegistrationsFromRoatp)
            {
                feedbackRatings.AddRange(providerRegistration.Feedback.FeedbackRating.Select(c =>
                    new ProviderRegistrationFeedbackRatingImport().Map(providerRegistration.Ukprn, c)));
                feedbackAttributes.AddRange(providerRegistration.Feedback.ProviderAttributes.Select(c =>
                    new ProviderRegistrationFeedbackAttributeImport().Map(providerRegistration.Ukprn, c)));
            }

            var providerRegistrationInsertTask = _providerRegistrationImportRepository.InsertMany(providerRegistrationImports);
            var feedbackRatingsInsertTask = _providerRegistrationFeedbackRatingImportRepository.InsertMany(feedbackRatings);
            var feedbackAttributesInsertTask =
                _providerRegistrationFeedbackAttributeImportRepository.InsertMany(feedbackAttributes);

            await Task.WhenAll(providerRegistrationInsertTask, feedbackRatingsInsertTask, feedbackAttributesInsertTask);
            return providerRegistrationImports;
        }

        private void ClearMainTables()
        {
            _providerRegistrationRepository.DeleteAll();
            _providerRegistrationFeedbackAttributeRepository.DeleteAll();
            _providerRegistrationFeedbackRatingRepository.DeleteAll();
        }

        private void ClearImportTables()
        {
            _providerRegistrationImportRepository.DeleteAll();
            _providerRegistrationFeedbackAttributeImportRepository.DeleteAll();
            _providerRegistrationFeedbackRatingImportRepository.DeleteAll();
        }
    }
}