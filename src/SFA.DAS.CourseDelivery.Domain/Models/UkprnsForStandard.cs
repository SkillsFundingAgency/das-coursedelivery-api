using System.Collections.Generic;

namespace SFA.DAS.CourseDelivery.Domain.Models
{
    public class UkprnsForStandard
    {
        public IEnumerable<int> UkprnsFilteredByStandardAndLocation { get; set; }
        public IEnumerable<int> UkprnsFilteredByStandard { get; set; }
    }
}