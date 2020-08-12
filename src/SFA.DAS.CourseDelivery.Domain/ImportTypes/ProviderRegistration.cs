using System;
using Newtonsoft.Json;

namespace SFA.DAS.CourseDelivery.Domain.ImportTypes
{
    public class ProviderRegistration
    {
        [JsonProperty("UKPRN")]
        public int Ukprn { get; set; }
        [JsonProperty("StatusDate")]
        public DateTime StatusDate { get; set; }
        [JsonProperty("StatusId")]
        public int StatusId { get; set; }
        [JsonProperty("OrganisationTypeId")]
        public int OrganisationTypeId { get; set; }
        [JsonProperty("ProviderTypeId")]
        public int ProviderTypeId { get; set; }
    }
}