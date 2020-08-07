using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using SFA.DAS.CourseDelivery.Domain.Extensions;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;

namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class NationalAchievementRateImport : NationalAchievementRateBase
    {
        public static implicit operator NationalAchievementRateImport(NationalAchievementRateCsv source)
        {
            var overallCohortResult = int.TryParse(source.OverallCohort, out var overallCohort);
            var overallAchievementRateResult = decimal.TryParse(source.OverallAchievementRate, out var overallAchievementRate);

            return new NationalAchievementRateImport
            {
                Ukprn = source.Ukprn,
                SectorSubjectArea = source.SectorSubjectArea,
                OverallCohort = !overallCohortResult ? (int?)null : overallCohort,
                OverallAchievementRate = !overallAchievementRateResult ? (decimal?)null : overallAchievementRate,
                Age = source.Age.ToAge(),
                ApprenticeshipLevel = source.ApprenticeshipLevel.ToApprenticeshipLevel(),
            };
        }

        
    }
}