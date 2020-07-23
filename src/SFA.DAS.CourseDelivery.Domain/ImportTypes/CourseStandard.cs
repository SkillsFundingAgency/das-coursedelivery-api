using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.CourseDelivery.Domain.ImportTypes
{
    public class CourseStandard
    {
        [JsonProperty("standardCode")]
        public long StandardCode { get; set; }

        [JsonProperty("locations")]
        public List<StandardLocation> Locations { get; set; }
    }
}