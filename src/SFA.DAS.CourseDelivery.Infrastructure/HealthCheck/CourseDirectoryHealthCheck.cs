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
    public class CourseDirectoryHealthCheck : IHealthCheck
    {
        private const string HealthCheckResultDescription = "Course Directory Health Check";
        private readonly IImportAuditRepository _importData;
        public CourseDirectoryHealthCheck(IImportAuditRepository importData)
        {
            _importData = importData;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var timer = Stopwatch.StartNew();
            var latestCourseDirectoryData = await _importData.GetLastImportByType(ImportType.CourseDirectory);

            timer.Stop();
            var durationString = timer.Elapsed.ToHumanReadableString();

            if (latestCourseDirectoryData == null)
            {
                return new HealthCheckResult(HealthStatus.Unhealthy, "No course directory data loaded", null, new Dictionary<string, object> { { "Duration", durationString } });
            }

            if (latestCourseDirectoryData.RowsImported == 0)
            {
                return new HealthCheckResult(HealthStatus.Unhealthy, "Course directory data load has imported zero rows", null, new Dictionary<string, object> { { "Duration", durationString } });
            }

            if (DateTime.UtcNow >= latestCourseDirectoryData.TimeStarted.AddHours(25))
            {
                return new HealthCheckResult(HealthStatus.Degraded, "Course directory data load is over 25 hours old", null, new Dictionary<string, object> { { "Duration", durationString } });
            }

            return HealthCheckResult.Healthy(HealthCheckResultDescription, new Dictionary<string, object> { { "Duration", durationString } });
        }
    }
}
