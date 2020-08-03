using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.CourseDelivery.Domain.ImportTypes
{
    public class CourseStandard
    {
        [JsonProperty("standardCode")]
        public int StandardCode { get; set; }
        [JsonProperty("standardInfoUrl")]
        public string StandardInfoUrl { get; set; }
        [JsonProperty("contact")]
        public CourseStandardContact Contact { get; set; }
        [JsonProperty("locations")]
        public List<StandardLocation> Locations { get; set; }
    }

    public class CourseStandardContact
    {
        [JsonProperty("contactUsUrl")]
        public string ContactUsUrl { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
    }
}