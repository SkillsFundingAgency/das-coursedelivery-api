using Microsoft.Extensions.Diagnostics.HealthChecks;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Courses.Infrastructure.HealthCheck;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.CourseDelivery.Infrastructure.HealthCheck
{
    public class ProviderRegistrationsHealthCheck : IHealthCheck
    {
        private const string HealthCheckResultDescription = "Provider Registrations Health Check";
        private readonly IImportAuditRepository _importData;
        public ProviderRegistrationsHealthCheck(IImportAuditRepository importData)
        {
            _importData = importData;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var timer = Stopwatch.StartNew();
            var latestProviderRegistrationsData = await _importData.GetLastImportByType(ImportType.ProviderRegistrations);

            timer.Stop();
            var durationString = timer.Elapsed.ToHumanReadableString();

            if (latestProviderRegistrationsData == null)
            {
                return new HealthCheckResult(HealthStatus.Degraded, "No provider registration data loaded", null, new Dictionary<string, object> { { "Duration", durationString } });
            }

            if (latestProviderRegistrationsData.RowsImported == 0)
            {
                return new HealthCheckResult(HealthStatus.Degraded, "Provider registrations data load has imported zero rows", null, new Dictionary<string, object> { { "Duration", durationString } });
            }

            if (DateTime.UtcNow >= latestProviderRegistrationsData.TimeStarted.AddHours(25))
            {
                return new HealthCheckResult(HealthStatus.Degraded, "Provider registrations data load is over 25 hours old", null, new Dictionary<string, object> { { "Duration", durationString } });
            }

            return HealthCheckResult.Healthy(HealthCheckResultDescription, new Dictionary<string, object> { 
                { "Duration", durationString }, { "FileName", latestProviderRegistrationsData.FileName.Split('\\').Last() } 
            });
        }
    }
}
