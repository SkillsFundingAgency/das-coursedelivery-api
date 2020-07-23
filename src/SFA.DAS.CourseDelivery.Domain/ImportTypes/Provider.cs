using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.CourseDelivery.Domain.ImportTypes
{
    public class Provider
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("ukprn")]
        public long Ukprn { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("nationalProvider")]
        public bool NationalProvider { get; set; }
        [JsonProperty("learnerSatisfaction")]
        public decimal LearnerSatisfaction { get; set; }
        [JsonProperty("employerSatisfaction")]
        public decimal EmployerSatisfaction { get; set; }
        [JsonProperty("standards")]
        public List<CourseStandard> Standards { get; set; }
        [JsonProperty("locations")]
        public List<CourseLocation> Locations { get; set; }
    }
}