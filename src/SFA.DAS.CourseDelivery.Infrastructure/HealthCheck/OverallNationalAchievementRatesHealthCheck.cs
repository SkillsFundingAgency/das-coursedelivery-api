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
    public class OverallNationalAchievementRatesHealthCheck : IHealthCheck
    {
        private const string HealthCheckResultDescription = "Overall National Achievement Rates Health Check";
        private readonly IImportAuditRepository _importData;
        public OverallNationalAchievementRatesHealthCheck(IImportAuditRepository importData)
        {
            _importData = importData;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var timer = Stopwatch.StartNew();
            var latestOverallNationalAchievementRatesData = await _importData.GetLastImportByType(ImportType.NationalAchievementRatesOverall);

            timer.Stop();
            var durationString = timer.Elapsed.ToHumanReadableString();

            if (latestOverallNationalAchievementRatesData == null)
            {
                return new HealthCheckResult(HealthStatus.Degraded, "No overall national achievement rates data loaded", null, new Dictionary<string, object> { { "Duration", durationString } });
            }

            if (latestOverallNationalAchievementRatesData.RowsImported == 0)
            {
                return new HealthCheckResult(HealthStatus.Degraded, "Overall National Achievement Rates data load has imported zero rows", null, new Dictionary<string, object> { { "Duration", durationString } });
            }

            return HealthCheckResult.Healthy(HealthCheckResultDescription, new Dictionary<string, object> { 
                { "Duration", durationString },
                { "FileName", latestOverallNationalAchievementRatesData.FileName.Split('\\').Last() }
            });
        }
    }
}
