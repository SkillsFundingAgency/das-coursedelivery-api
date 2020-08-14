using MediatR;

namespace SFA.DAS.CourseDelivery.Application.OverallNationalAchievementRates.Queries
{
    public class GetOverallAchievementRatesQuery : IRequest<GetOverallAchievementRatesResponse>
    {
        public string SectorSubjectArea { get ; set ; }
    }
}