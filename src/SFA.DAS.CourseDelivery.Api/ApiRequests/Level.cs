using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.CourseDelivery.Api.ApiRequests
{
    public enum Level : short
    {
        
        Unknown = 0,
        AllLevels = 1,
        Two = 2,
        Three = 3,
        FourPlus = 4
    }
}