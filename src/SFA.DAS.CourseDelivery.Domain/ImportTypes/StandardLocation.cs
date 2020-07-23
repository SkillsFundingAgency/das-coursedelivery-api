using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.CourseDelivery.Domain.ImportTypes
{
    public class StandardLocation
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("deliveryModes")]
        public List<string> DeliveryModes { get; set; }

        [JsonProperty("radius")]
        public long Radius { get; set; }
    }
}