using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.CourseDelivery.Domain.ImportTypes
{
    public class Provider
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("ukprn")]
        public int Ukprn { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("learnerSatisfaction")]
        public decimal? LearnerSatisfaction { get; set; }
        [JsonProperty("employerSatisfaction")]
        public decimal? EmployerSatisfaction { get; set; }
        [JsonProperty("tradingName")]
        public string TradingName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("website")]
        public string Website { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("standards")]
        public List<CourseStandard> Standards { get; set; }
        [JsonProperty("locations")]
        public List<CourseLocation> Locations { get; set; }
        [JsonProperty("marketingInfo")]
        public string MarketingInfo { get; set; }
    }
}