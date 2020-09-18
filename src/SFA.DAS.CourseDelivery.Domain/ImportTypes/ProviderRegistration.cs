using System;
using System.Collections.Generic;
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
        [JsonProperty("feedback")]
        public Feedback Feedback { get; set; }
    }
    public class Feedback
    {
        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("feedbackRating")]
        public List<FeedbackRating> FeedbackRating { get; set; }

        [JsonProperty("providerAttributes")]
        public List<ProviderAttribute> ProviderAttributes { get; set; }
    }

    public class FeedbackRating
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }
    }

    public class ProviderAttribute
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("strengths")]
        public long Strengths { get; set; }

        [JsonProperty("weaknesses")]
        public long Weaknesses { get; set; }
    }
    public class RoatpTypeConstants
    {
        public const int StatusOfActive = 1;
        public const int ProviderTypeOfMainProvider = 1;
    }
}