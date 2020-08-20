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
    public class NationalAchievementRatesHealthCheck : IHealthCheck
    {
        private const string HealthCheckResultDescription = "National Achievement Rates Health Check";
        private readonly IImportAuditRepository _importData;
        public NationalAchievementRatesHealthCheck(IImportAuditRepository importData)
        {
            _importData = importData;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var timer = Stopwatch.StartNew();
            var latestNationalAchievementRatesData = await _importData.GetLastImportByType(ImportType.NationalAchievementRates);

            timer.Stop();
            var durationString = timer.Elapsed.ToHumanReadableString();

            if (latestNationalAchievementRatesData == null)
            {
                return new HealthCheckResult(HealthStatus.Degraded, "No national achievement rates data loaded", null, new Dictionary<string, object> { { "Duration", durationString } });
            }

            if (latestNationalAchievementRatesData.RowsImported == 0)
            {
                return new HealthCheckResult(HealthStatus.Degraded, "National Achievement Rates data load has imported zero rows", null, new Dictionary<string, object> { { "Duration", durationString } });
            }

            return HealthCheckResult.Healthy(HealthCheckResultDescription, new Dictionary<string, object> { 
                { "Duration", durationString },
                { "FileName", latestNationalAchievementRatesData.FileName.Split('\\').Last() },
            });
        }
    }
}
