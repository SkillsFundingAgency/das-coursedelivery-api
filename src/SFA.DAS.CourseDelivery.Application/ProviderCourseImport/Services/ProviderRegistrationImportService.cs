using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using ProviderRegistration = SFA.DAS.CourseDelivery.Domain.ImportTypes.ProviderRegistration;

namespace SFA.DAS.CourseDelivery.Application.ProviderCourseImport.Services
{
    public class ProviderRegistrationImportService : IProviderRegistrationImportService
    {
        private readonly ILogger<ProviderRegistrationImportService> _logger;
        private readonly IRoatpApiService _roatpApiService;
        private readonly IApprenticeFeedbackAttributesApiService _apprenticeFeedbackAttributesApiService;
        private readonly IProviderRegistrationImportRepository _providerRegistrationImportRepository;
        private readonly IProviderRegistrationRepository _providerRegistrationRepository;
        private readonly IApprenticeFeedbackAttributesImportRepository _apprenticeFeedbackAttributesImportRepository;
        private readonly IApprenticeFeedbackAttributesRepository _apprenticeFeedbackAttributesRepository;
        private readonly IProviderRegistrationFeedbackAttributeRepository _providerRegistrationFeedbackAttributeRepository;
        private readonly IProviderRegistrationFeedbackAttributeImportRepository _providerRegistrationFeedbackAttributeImportRepository;
        private readonly IProviderRegistrationFeedbackRatingRepository _providerRegistrationFeedbackRatingRepository;
        private readonly IProviderRegistrationFeedbackRatingImportRepository _providerRegistrationFeedbackRatingImportRepository;
        private readonly IImportAuditRepository _auditRepository;

        public ProviderRegistrationImportService(
            ILogger<ProviderRegistrationImportService> logger,
            IRoatpApiService roatpApiService,
            IApprenticeFeedbackAttributesApiService apprenticeFeedbackAttributesApiService,
            IProviderRegistrationImportRepository providerRegistrationImportRepository,
            IProviderRegistrationRepository providerRegistrationRepository,
            IApprenticeFeedbackAttributesImportRepository apprenticeFeedbackAttributesImportRepository,
            IApprenticeFeedbackAttributesRepository apprenticeFeedbackAttributesRepository,
            IProviderRegistrationFeedbackAttributeRepository providerRegistrationFeedbackAttributeRepository,
            IProviderRegistrationFeedbackAttributeImportRepository providerRegistrationFeedbackAttributeImportRepository,
            IProviderRegistrationFeedbackRatingRepository providerRegistrationFeedbackRatingRepository,
            IProviderRegistrationFeedbackRatingImportRepository providerRegistrationFeedbackRatingImportRepository,
            IImportAuditRepository auditRepository)
        {
            _logger = logger;
            _roatpApiService = roatpApiService;
            _apprenticeFeedbackAttributesApiService = apprenticeFeedbackAttributesApiService;
            _providerRegistrationImportRepository = providerRegistrationImportRepository;
            _providerRegistrationRepository = providerRegistrationRepository;
            _apprenticeFeedbackAttributesImportRepository = apprenticeFeedbackAttributesImportRepository;
            _apprenticeFeedbackAttributesRepository = apprenticeFeedbackAttributesRepository;
            _providerRegistrationFeedbackAttributeRepository = providerRegistrationFeedbackAttributeRepository;
            _providerRegistrationFeedbackAttributeImportRepository = providerRegistrationFeedbackAttributeImportRepository;
            _providerRegistrationFeedbackRatingRepository = providerRegistrationFeedbackRatingRepository;
            _providerRegistrationFeedbackRatingImportRepository = providerRegistrationFeedbackRatingImportRepository;
            _auditRepository = auditRepository;
        }

        public async Task ImportData()
        {
            var importStartTime = DateTime.UtcNow;

            var providerRegistrationsFromRoatp = (await _roatpApiService.GetProviderRegistrations()).ToList(); //list of providers + feedback

            var attributesFromApprenticeFeedback = (await _apprenticeFeedbackAttributesApiService.GetApprenticeFeedbackAttributes()).ToList();

            if (providerRegistrationsFromRoatp.Count == 0 || attributesFromApprenticeFeedback.Count == 0)
            {
                _logger.LogInformation("No data received from ROATP or from Apprentice Feedback. Ending import.");
                return;
            }

            _logger.LogInformation("Clearing import table");
            ClearImportTables();
            _logger.LogInformation("Populate import table");
            
            var providerRegistrationImports = await PopulateImportTables(providerRegistrationsFromRoatp, attributesFromApprenticeFeedback);

            _logger.LogInformation("Clearing data table");
            ClearMainTables();
            _logger.LogInformation("Populate data table");

            await PopulateMainTables();

            await _auditRepository.Insert(new ImportAudit(
                importStartTime,
                providerRegistrationImports.Count, 
                ImportType.ProviderRegistrations));
        }

        private async Task PopulateMainTables()
        {
            var feedbackRating = _providerRegistrationFeedbackRatingImportRepository.GetAll();
            var feedbackAttributes = _providerRegistrationFeedbackAttributeImportRepository.GetAll();
            var providerRegistrations = _providerRegistrationImportRepository.GetAll();
            var apprenticeFeedbackAttributes = _apprenticeFeedbackAttributesImportRepository.GetAll();

            await Task.WhenAll(feedbackRating, feedbackAttributes, providerRegistrations);

            var insertProviderTask = _providerRegistrationRepository.InsertMany(providerRegistrations.Result
                .Select(c=>(Domain.Entities.ProviderRegistration)c).ToList());
            var insertProviderRatingTask =
                _providerRegistrationFeedbackRatingRepository.InsertMany(feedbackRating.Result
                    .Select(c => (ProviderRegistrationFeedbackRating) c).ToList());
            var insertProviderAttributeTask =
                _providerRegistrationFeedbackAttributeRepository.InsertMany(feedbackAttributes.Result
                    .Select(c => (ProviderRegistrationFeedbackAttribute) c).ToList());
            var insertApprenticeFeedbackAttributesTask =
                _apprenticeFeedbackAttributesRepository.InsertMany(apprenticeFeedbackAttributes.Result
                    .Select(c => (ApprenticeFeedbackAttributes)c).ToList());

            await Task.WhenAll(insertProviderTask, insertProviderAttributeTask, insertProviderRatingTask, insertApprenticeFeedbackAttributesTask);
        }

        private async Task<List<ProviderRegistrationImport>> PopulateImportTables(IReadOnlyCollection<ProviderRegistration> providerRegistrationsFromRoatp, 
            IReadOnlyCollection<ApprenticeFeedbackAttribute> apprenticeFeedbackAttributesFromApprenticeFeedback)
        {
            var providerRegistrationImports = providerRegistrationsFromRoatp
                .Select(registration => (ProviderRegistrationImport) registration)
                .ToList();

            var apprenticeFeedbackAttributesImports = apprenticeFeedbackAttributesFromApprenticeFeedback
                .Select(attribute => (ApprenticeFeedbackAttributesImport)attribute)
                .ToList();

            var feedbackRatings = new List<ProviderRegistrationFeedbackRatingImport>();
            var feedbackAttributes = new List<ProviderRegistrationFeedbackAttributeImport>();

            foreach (var providerRegistration in providerRegistrationsFromRoatp)
            {
                if (providerRegistration.Feedback.FeedbackRating != null)
                {
                    feedbackRatings.AddRange(providerRegistration.Feedback.FeedbackRating.Select(c =>
                        new ProviderRegistrationFeedbackRatingImport().Map(providerRegistration.Ukprn, c)));    
                }
                if (providerRegistration.Feedback.ProviderAttributes != null)
                {
                    feedbackAttributes.AddRange(providerRegistration.Feedback.ProviderAttributes.Select(c =>
                        new ProviderRegistrationFeedbackAttributeImport().Map(providerRegistration.Ukprn, c)));    
                }
            }

            var providerRegistrationInsertTask = _providerRegistrationImportRepository.InsertMany(providerRegistrationImports);
            var apprenticeFeedbackAttributesInsertTask = _apprenticeFeedbackAttributesImportRepository.InsertMany(apprenticeFeedbackAttributesImports);
            var feedbackRatingsInsertTask = _providerRegistrationFeedbackRatingImportRepository.InsertMany(feedbackRatings);
            var feedbackAttributesInsertTask = _providerRegistrationFeedbackAttributeImportRepository.InsertMany(feedbackAttributes);

            await Task.WhenAll(providerRegistrationInsertTask, apprenticeFeedbackAttributesInsertTask, feedbackRatingsInsertTask, feedbackAttributesInsertTask);
            return providerRegistrationImports;
        }

        private void ClearMainTables()
        {
            _providerRegistrationRepository.DeleteAll();
            _providerRegistrationFeedbackAttributeRepository.DeleteAll();
            _providerRegistrationFeedbackRatingRepository.DeleteAll();
            _apprenticeFeedbackAttributesRepository.DeleteAll();
        }

        private void ClearImportTables()
        {
            _providerRegistrationImportRepository.DeleteAll();
            _providerRegistrationFeedbackAttributeImportRepository.DeleteAll();
            _providerRegistrationFeedbackRatingImportRepository.DeleteAll();
            _apprenticeFeedbackAttributesImportRepository.DeleteAll();
        }
    }
}